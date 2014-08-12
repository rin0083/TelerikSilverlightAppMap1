using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Map;
namespace TelerikSilverlightAppMap1.MapHelper
{
    public class BingRoute
    {
        private const int BatchSize = 15;

		private BingRouteProvider bingRouteProvider;
		private int locationIndex;
		private Collection<RouteResponse> responses;

        public BingRoute(BingRouteProvider bingRouteProvider)
		{
			this.bingRouteProvider = bingRouteProvider;
			this.bingRouteProvider.RoutingCompleted += new EventHandler<RoutingCompletedEventArgs>(bingRouteProvider_RoutingCompleted);
		}

		public event EventHandler<RoutingCompletedEventArgs> RoutingCompleted;

		public LocationCollection Locations
		{
			get;
			private set;
		}

		private bool IsAllPartsComplete
		{
			get
			{
				return this.locationIndex >= this.Locations.Count - 1;
			}
		}

		internal void RequestRoute(LocationCollection locations)
		{
			this.Locations = locations;
			this.locationIndex = 0;
			this.responses = new Collection<RouteResponse>();
			this.NextRouteRequest();
		}

		private void NextRouteRequest()
		{
			RouteRequest routeRequest = new RouteRequest()
			{
				Culture = new System.Globalization.CultureInfo("en-US")
			};

			routeRequest.Options.RoutePathType = RoutePathType.Points;

			this.SetWayPoints(routeRequest);
			this.bingRouteProvider.CalculateRouteAsync(routeRequest);
		}

		private void SetWayPoints(RouteRequest routeRequest)
		{
			if (this.locationIndex > 0)
			{
				this.locationIndex--;
			}

			int endPartIndex = this.locationIndex + BatchSize;
			for (; this.locationIndex < endPartIndex
				&& this.locationIndex < this.Locations.Count;
				this.locationIndex++)
			{
				routeRequest.Waypoints.Add(this.Locations[this.locationIndex]);
			}
		}

		private void bingRouteProvider_RoutingCompleted(object sender, RoutingCompletedEventArgs e)
		{
			RouteResponse routeResponse = e.Response as RouteResponse;
			if (routeResponse != null)
			{
				if (routeResponse.Error != null)
				{
					this.CompleteRequest(e);
					return;
				}

				RouteResult result = routeResponse.Result;
				if (result != null && result.RoutePath != null)
				{
					this.responses.Add(routeResponse);
				}

				this.Continue();
			}
		}

		private void Continue()
		{
			if (this.IsAllPartsComplete)
			{
				RoutingCompletedEventArgs e = this.MergeResponses();
				this.CompleteRequest(e);
			}
			else
			{
				this.NextRouteRequest();
			}
		}

		private RoutingCompletedEventArgs MergeResponses()
		{
			RoutingCompletedEventArgs e = new RoutingCompletedEventArgs()
			{
				Response = this.responses[0]
			};

			RoutePath routePath = this.responses[0].Result.RoutePath;
			RouteLegCollection legs = this.responses[0].Result.Legs;
			RouteSummary summary = this.responses[0].Result.Summary;

			for (int responseIndex = 1; responseIndex < this.responses.Count; responseIndex++)
			{
				RouteResponse routeResponse = this.responses[responseIndex];
				RouteResult result = routeResponse.Result;
				foreach (Location location in result.RoutePath.Points)
				{
					routePath.Points.Add(location);
				}

				foreach (RouteLeg leg in result.Legs)
				{
					legs.Add(leg);
				}

				this.MergeSummary(summary, result.Summary);
			}

			return e;
		}

		private void MergeSummary(RouteSummary summary, RouteSummary routeSummary)
		{
			summary.Distance += routeSummary.Distance;
			summary.TimeInSeconds += routeSummary.TimeInSeconds;

			double north = Math.Max(summary.BoundingRectangle.North, routeSummary.BoundingRectangle.North);
			double south = Math.Min(summary.BoundingRectangle.South, routeSummary.BoundingRectangle.South);
			double west = Math.Min(summary.BoundingRectangle.West, routeSummary.BoundingRectangle.West);
			double east = Math.Max(summary.BoundingRectangle.East, routeSummary.BoundingRectangle.East);

			LocationRect bounds = new LocationRect(new Location(north, west), new Location(south, east));
			summary.BoundingRectangle = bounds;
		}

		private void CompleteRequest(RoutingCompletedEventArgs e)
		{
			EventHandler<RoutingCompletedEventArgs> complete = this.RoutingCompleted;
			if (complete != null)
			{
				complete(this, e);
			}
		}
    }
}
