﻿#pragma checksum "..\..\Runner.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "610195076F075E8C4557DBB61958852A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using net.PaulChristensen.TestRunnerW32.Properties;


namespace net.PaulChristensen.TestRunnerW32 {
    
    
    /// <summary>
    /// Runner
    /// </summary>
    public partial class Runner : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\Runner.xaml"
        internal net.PaulChristensen.TestRunnerW32.Runner MainTestRunner;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Runner.xaml"
        internal System.Windows.Controls.Grid BaseWindowGrid;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Runner.xaml"
        internal System.Windows.Controls.Menu MainMenu;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\Runner.xaml"
        internal System.Windows.Controls.StackPanel StackPanelLeft;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Runner.xaml"
        internal System.Windows.Controls.Button ClearQueButton;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Runner.xaml"
        internal System.Windows.Controls.Button ExecuteTestsButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\Runner.xaml"
        internal System.Windows.Controls.Button SaveSuiteButton;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Runner.xaml"
        internal System.Windows.Controls.Button QueAllTests;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\Runner.xaml"
        internal System.Windows.Controls.GridSplitter HorizontalGridSplitter;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\Runner.xaml"
        internal System.Windows.Controls.Grid TopGrid;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Runner.xaml"
        internal System.Windows.Controls.GridSplitter VerticalGridSplitter;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Runner.xaml"
        internal System.Windows.Controls.ListView AvailableTests;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\Runner.xaml"
        internal System.Windows.Controls.ListView ExecutionList;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\Runner.xaml"
        internal System.Windows.Controls.ListView ResultsView;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\Runner.xaml"
        internal System.Windows.Controls.Primitives.StatusBar StatusBar;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/net.PaulChristensen.TestRunnerW32;component/runner.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Runner.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainTestRunner = ((net.PaulChristensen.TestRunnerW32.Runner)(target));
            
            #line 11 "..\..\Runner.xaml"
            this.MainTestRunner.Loaded += new System.Windows.RoutedEventHandler(this.WithXaml_Load);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BaseWindowGrid = ((System.Windows.Controls.Grid)(target));
            
            #line 12 "..\..\Runner.xaml"
            this.BaseWindowGrid.GotMouseCapture += new System.Windows.Input.MouseEventHandler(this.AvailableTests_PreviewMouseMove);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MainMenu = ((System.Windows.Controls.Menu)(target));
            return;
            case 4:
            this.StackPanelLeft = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.ClearQueButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\Runner.xaml"
            this.ClearQueButton.Click += new System.Windows.RoutedEventHandler(this.ClearQueButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ExecuteTestsButton = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\Runner.xaml"
            this.ExecuteTestsButton.Click += new System.Windows.RoutedEventHandler(this.ExecuteTestsButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SaveSuiteButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\Runner.xaml"
            this.SaveSuiteButton.Click += new System.Windows.RoutedEventHandler(this.SaveSuiteButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.QueAllTests = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\Runner.xaml"
            this.QueAllTests.Click += new System.Windows.RoutedEventHandler(this.QueAllTests_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.HorizontalGridSplitter = ((System.Windows.Controls.GridSplitter)(target));
            return;
            case 10:
            this.TopGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 11:
            this.VerticalGridSplitter = ((System.Windows.Controls.GridSplitter)(target));
            return;
            case 12:
            this.AvailableTests = ((System.Windows.Controls.ListView)(target));
            
            #line 47 "..\..\Runner.xaml"
            this.AvailableTests.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.AvailableTests_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 48 "..\..\Runner.xaml"
            this.AvailableTests.PreviewMouseMove += new System.Windows.Input.MouseEventHandler(this.AvailableTests_PreviewMouseMove);
            
            #line default
            #line hidden
            
            #line 49 "..\..\Runner.xaml"
            this.AvailableTests.MouseMove += new System.Windows.Input.MouseEventHandler(this.AvailableTests_PreviewMouseMove);
            
            #line default
            #line hidden
            
            #line 49 "..\..\Runner.xaml"
            this.AvailableTests.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.AvailableTests_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 13:
            this.ExecutionList = ((System.Windows.Controls.ListView)(target));
            
            #line 61 "..\..\Runner.xaml"
            this.ExecutionList.Drop += new System.Windows.DragEventHandler(this.ExecutionList_Drop);
            
            #line default
            #line hidden
            
            #line 62 "..\..\Runner.xaml"
            this.ExecutionList.DragEnter += new System.Windows.DragEventHandler(this.ExecutionList_DragEnter);
            
            #line default
            #line hidden
            return;
            case 14:
            this.ResultsView = ((System.Windows.Controls.ListView)(target));
            
            #line 77 "..\..\Runner.xaml"
            this.ResultsView.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ResultsView_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 77 "..\..\Runner.xaml"
            this.ResultsView.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ResultsView_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 15:
            this.StatusBar = ((System.Windows.Controls.Primitives.StatusBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}