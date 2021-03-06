﻿using System.Web;
using System.Web.Optimization;

namespace ReadMe.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/error-css").Include(
                "~/Content/bootstrap.css",
                "~/Content/error.css"));

            bundles.Add(new StyleBundle("~/Content/admin-css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/admin.css"));

            bundles.Add(new StyleBundle("~/Content/home-css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/homepage.css"));

            bundles.Add(new ScriptBundle("~/bundles/error").Include(
                //"~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery-ui-1.12.1.min.js",
                "~/Scripts/Custom/error.js"));

            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                     //"~/Scripts/jquery.unobtrusive-ajax.min.js",
                     "~/Scripts/Custom/search.js"));

            bundles.Add(new ScriptBundle("~/bundles/reviews").Include(
                     //"~/Scripts/jquery.unobtrusive-ajax.min.js",
                     "~/Scripts/Custom/reviews.js"));

            bundles.Add(new ScriptBundle("~/bundles/book-status").Include(
                     //"~/Scripts/jquery.unobtrusive-ajax.min.js",
                     "~/Scripts/Custom/book-status.js"));

            bundles.Add(new ScriptBundle("~/bundles/user-profile").Include(
                     //"~/Scripts/jquery.unobtrusive-ajax.min.js",
                     "~/Scripts/Custom/user-profile.js"));

            bundles.Add(new ScriptBundle("~/bundles/rating").Include(
                     //"~/Scripts/jquery.unobtrusive-ajax.min.js",
                     "~/Scripts/Custom/book-rating.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin-area").Include(
                     //"~/Scripts/jquery.unobtrusive-ajax.min.js",
                     "~/Scripts/Custom/admin-area.js"));
        }
    }
}
