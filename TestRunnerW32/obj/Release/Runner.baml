   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  \ Xnet.PaulChristensen.TestRunnerW32, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:net.PaulChristensen.TestRunnerW32.Properties,net.PaulChristensen.TestRunnerW32.Properties .   (net.PaulChristensen.TestRunnerW32.Runner   ^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     9System&clr-namespace:System;assembly=mscorlib     8x,http://schemas.microsoft.com/winfx/2006/xaml    O
properties:clr-namespace:net.PaulChristensen.TestRunnerW32.Properties    :my-http://schemas.microsoft.com/wpf/2008/toolkit    -     �Name$  MainTestRunner��$	��427��$	��737�� � Title$ Main Test Runner��.��� -    �Name$ BaseWindowGrid�� � RowDefinitions �� $
��Auto �$��20���� $��70���� $
��Auto �$��5���� $��70���� $
��Auto �$��20�� � ColumnDefinitions �� $
��Auto �$	��120���� $��* �.+�o� -   $��MainMenu��$��20��$	��Top=�$��2��$��0,0,0,0q��� -   $��StackPanelLeft��$
��Left=�$	��120��$��1��$��3��.+��� -   $��ClearQueButton��$��28��$
��Auto��;  5net.PaulChristensen.TestRunnerW32.Properties.Resource   ClearTestQueButtonText#��Z �� -   $��ExecuteTestsButton��$��28��$
��Auto��   ExecuteTestsButtonText#��Z �� -   $��SaveSuiteButton��$��28��$
��Auto��$��$Save Suite$���� -   $��QueAllTests��$��28��$
��Auto��$��$QueAllTests$���� -	   $��HorizontalGridSplitter��$��1��$��Stretch=�$��0q� �� ResizeDirection$
 Rows=�$��2��$��5��$��2��� -
   $��TopGrid��$��1��$��1��$��0,-1,0,0q�$��2�� �� $��* ��� $
��Auto ��� $��* �.+��� -   $��VerticalGridSplitter��$��Stretch=�$��0q�$��3��$ Columns=�$��1��$��Stretch=��� -    ��Name$ AvailableTests��$	��1,1q�$	 �� IsSynchronizedWithCurrentItem$
	 TrueY�
 J� 	AllowDrop
 .  �� SelectionMode$ Single=�$��0�� �� View �� .P��� 
 X� NaN �� Width# Z    TestNameColumnHeaderText#��Z  �� DisplayMemberBinding �� *
TestName+�� # Z &   TestDescriptionColumnHeaderText#��Z  �� *TestDescription+�� -   $ ExecutionList��$	��1,1q�$
	 TrueY�
 . $��2�� �� .P��� # Z #��Z  �� *
TestName+�� # Z #��Z  �� *TestDescription+�� -   $��ResultsView��$��1,1,1,1q�$
	 TrueY� �� AlternationCount$ 1��$��3��$��1��$
��Auto�� �� .P��� # Z    TestIdColumnHeaderText#��Z  �� *CurrentTestId+�� $	 120��#��Z  �� *CurrentTestName+�� $	 200��!   TestStatusColumnHeaderText#��Z  �� *CurrentTestStatus+�� $	 300��"   TestResultsColumnHeaderText#��Z  �� *CurrentTestResults+�� -   $��	StatusBar��$��20��$	��Top=�$��4��$��2��.@���    TotalTestCountStatusText#��Z �� ����  �� Path$ TestCountString��� �� $��$Run Test Count$:���� ���� $ RunTestCountString��� $   ExecutingTestNumberStatusText#��Z �� ���� $ CurrentTestCountString��� $
�� Of ���� ���� $ RunTestCountString��� �� $��Stretch=�$��Stretch=�.��� -   $��ProgSuiteProgress�� � Value �� *SuiteProgressPercent+ �� UpdateSourceTrigger$ PropertyChanged=�