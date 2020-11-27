using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItSoftware.Core.Exception
{
    public class ItsExceptionRenderExtension
    {
        public static List<ItsExceptionRenderExtension> RenderExtensions { get; } = new List<ItsExceptionRenderExtension>();
        static ItsExceptionRenderExtension()
        {
            //
            // DbEntityValidationException
            //
            var ext1 = new ItsExceptionRenderExtension()
            {
                FullName = "System.Data.Entity.Validation.DbEntityValidationException",
                Header = "DbEntityValidationException"
            };
            var prop1 = new ItsExceptionRenderPropertyExtension() {
                Name = "EntityValidationErrors",
                IsEnumerable = true,
            };
            var prop1s1 = new ItsExceptionRenderPropertyExtension()
            {
                Name = "ValidationErrors",
                IsEnumerable = true,
            };
            var prop1s1s1 = new ItsExceptionRenderPropertyExtension()
            {
                Name = "PropertyName",
                IsEnumerable = false,
            };
            var prop1s1s2 = new ItsExceptionRenderPropertyExtension()
            {
                Name = "ErrorMessage",
                IsEnumerable = false,
            };
            prop1s1.Properties.Add(prop1s1s1);
            prop1s1.Properties.Add(prop1s1s2);
            prop1.Properties.Add(prop1s1);
            ext1.Properties.Add(prop1);
            ItsExceptionRenderExtension.RenderExtensions.Add(ext1);
            //-------------------------------------------------------------------------
        }
        public static string Render(System.Exception x)
        {
            var output = new StringBuilder();
            output.AppendLine();

            try
            {


                foreach (var render in ItsExceptionRenderExtension.RenderExtensions)
                {
                    output.AppendLine("#####################################");
                    output.AppendLine($"## {render.Header}");
                    output.AppendLine("##");

                    var t = x.GetType();
                    if (t.FullName == render.FullName)
                    {
                        foreach (var p in render.Properties)
                        {
                            output.Append(ItsExceptionRenderExtension.RenderProperty(x, t, p, x));
                        }
                    }
                }
            }
            catch ( System.Exception y )
            {
                output.Append(y.ToString());
            }

            return output.ToString();
        }
        public static string RenderProperty(System.Exception x, Type t, ItsExceptionRenderPropertyExtension prop, object obj)
        {
            var output = new StringBuilder();

            if (prop.IsEnumerable)
            {
                var propp = t.GetProperty(prop.Name);
                var propv = propp.GetValue(obj);
                var propie = propv as IEnumerable;
                foreach (var pie in propie)
                {
                    foreach (var p in prop.Properties)
                    {
                        output.Append(ItsExceptionRenderExtension.RenderProperty(x, pie.GetType(), p, pie));
                    }
                }
            }
            else
            {
                var propv = t.GetProperty(prop.Name);
                var val = propv.GetValue(obj);
                output.AppendLine($"{prop.Name} = {val}");

                foreach (var p in prop.Properties)
                {
                    output.Append(ItsExceptionRenderExtension.RenderProperty(x, t, p, obj));
                }
            }

            return output.ToString();
        }

        public string FullName { get; set; } = string.Empty;
        public string Header { get; set; } = string.Empty;
        public List<ItsExceptionRenderPropertyExtension> Properties { get; } = new List<ItsExceptionRenderPropertyExtension>();
    }
}
