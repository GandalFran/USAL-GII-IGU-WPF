﻿#pragma checksum "..\..\..\..\..\src\view\Windows\FunctionListWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "07A292D02CA013CCF360392C0E08E365BC64927E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using IGUWPF.src.view.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace IGUWPF.src.view.Windows {
    
    
    /// <summary>
    /// FunctionListWindow
    /// </summary>
    public partial class FunctionListWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\..\src\view\Windows\FunctionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem SettingsButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\src\view\Windows\FunctionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem SaveFileButton;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\src\view\Windows\FunctionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem OpenFileButton;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\src\view\Windows\FunctionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid FunctionListPanel;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\..\src\view\Windows\FunctionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu DataGridContextMenu;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\src\view\Windows\FunctionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem DataGridContextMenu_AddFunction;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\..\src\view\Windows\FunctionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem DataGridContextMenu_DeleteSelectedFunction;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\..\src\view\Windows\FunctionListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem DataGridContextMenu_EditSelectedFunction;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/IGUWPF;component/src/view/windows/functionlistwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\src\view\Windows\FunctionListWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.SettingsButton = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 2:
            this.SaveFileButton = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 3:
            this.OpenFileButton = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 4:
            this.FunctionListPanel = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.DataGridContextMenu = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 6:
            this.DataGridContextMenu_AddFunction = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 7:
            this.DataGridContextMenu_DeleteSelectedFunction = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 8:
            this.DataGridContextMenu_EditSelectedFunction = ((System.Windows.Controls.MenuItem)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

