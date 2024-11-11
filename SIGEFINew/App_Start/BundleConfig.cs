using System.Web.Optimization;

namespace SIGEFINew
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      //"~/content/dist/js/adminlte.min.js",
                      "~/content/bower_components/jquery/dist/jquery.min.js",
                      "~/content/bower_components/bootstrap/dist/js/bootstrap.min.js",
                      "~/content/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/content/bower_components/fastclick/lib/fastclick.js",
                      "~/content/bower_components/morris.js/morris.min.js",
                      "~/content/dist/js/demo.js",
                      //"~/Scripts/jquery-{version}.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bower_components/font-awesome/css/font-awesome.min.css",
                      "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/MiEstilo.css",
                      "~/Content/dist/css/AdminLTE.min.css",
                      "~/Content/content/dist/css/skins/_all-skins.min.css",
                      "~/Content/dist/css/skins/_all-skins.min.css",
                      "~/Content/bower_components/Ionicons/css/ionicons.min.css",
                      "~/Content/css/select2")
                      );

            bundles.Add(new ScriptBundle("~/bundles/datatablesjs").Include(
                  "~/Scripts/jquery.datatables.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatablescss").Include(
                        "~/Content/datatables.css"));


        }
    }
}
