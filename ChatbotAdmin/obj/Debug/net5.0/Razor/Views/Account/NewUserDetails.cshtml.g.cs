#pragma checksum "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c69ec718571e1ecd3771720ff001ea6a64be0cd5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_NewUserDetails), @"mvc.1.0.view", @"/Views/Account/NewUserDetails.cshtml")]
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
#line 1 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\_ViewImports.cshtml"
using ChatbotAdmin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\_ViewImports.cshtml"
using ChatbotAdmin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c69ec718571e1ecd3771720ff001ea6a64be0cd5", @"/Views/Account/NewUserDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c9ea04d4691d1f29dc3597655f80bc49ac576ce5", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_NewUserDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ChatbotAdmin.Models.ClubUser>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
  
    ViewData["Title"] = "User Details";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div>\r\n    <h4>New User Details</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 14 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 17 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 20 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 23 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayFor(model => model.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 26 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 29 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 32 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 35 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayFor(model => model.PhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 38 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.Sex));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 41 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayFor(model => model.Sex));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 44 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.DateOfBirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 47 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayFor(model => model.DateOfBirth));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
#nullable restore
#line 50 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayNameFor(model => model.HashPassword));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
#nullable restore
#line 53 "C:\Project\botService\ChatbotAdmin\ChatbotAdmin\Views\Account\NewUserDetails.cshtml"
       Write(Html.DisplayFor(model => model.HashPassword));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ChatbotAdmin.Models.ClubUser> Html { get; private set; }
    }
}
#pragma warning restore 1591
