#pragma checksum "D:\Users\Johan\MyDocuments\GitHub\HRSystemCore\HRTestSYSTEMCoreWeb\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9a24ffafba8558ad407865fe3497b74c2a2afb22"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Users\Johan\MyDocuments\GitHub\HRSystemCore\HRTestSYSTEMCoreWeb\Views\_ViewImports.cshtml"
using HRSystemCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Users\Johan\MyDocuments\GitHub\HRSystemCore\HRTestSYSTEMCoreWeb\Views\_ViewImports.cshtml"
using HRSystemCore.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a24ffafba8558ad407865fe3497b74c2a2afb22", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"581d575a2f9014c9a0c47c066fd142aff4a1eaf8", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\Users\Johan\MyDocuments\GitHub\HRSystemCore\HRTestSYSTEMCoreWeb\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-center\">\r\n    ");
#nullable restore
#line 6 "D:\Users\Johan\MyDocuments\GitHub\HRSystemCore\HRTestSYSTEMCoreWeb\Views\Home\Index.cshtml"
Write(Html.ActionLink("List of Regions", "Index", "Regions"));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n    ");
#nullable restore
#line 7 "D:\Users\Johan\MyDocuments\GitHub\HRSystemCore\HRTestSYSTEMCoreWeb\Views\Home\Index.cshtml"
Write(Html.ActionLink("List of Companies", "Index", "Companies"));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n    ");
#nullable restore
#line 8 "D:\Users\Johan\MyDocuments\GitHub\HRSystemCore\HRTestSYSTEMCoreWeb\Views\Home\Index.cshtml"
Write(Html.ActionLink("List of Departments", "Index", "Departments"));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n    ");
#nullable restore
#line 9 "D:\Users\Johan\MyDocuments\GitHub\HRSystemCore\HRTestSYSTEMCoreWeb\Views\Home\Index.cshtml"
Write(Html.ActionLink("List of People/Resource", "Index", "People"));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n    ");
#nullable restore
#line 10 "D:\Users\Johan\MyDocuments\GitHub\HRSystemCore\HRTestSYSTEMCoreWeb\Views\Home\Index.cshtml"
Write(Html.ActionLink("List of Status's", "Index", "Status"));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
