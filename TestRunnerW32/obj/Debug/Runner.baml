   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      \ Xnet.PaulChristensen.TestRunnerW32, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:net.PaulChristensen.TestRunnerW32.Properties,net.PaulChristensen.TestRunnerW32.Properties 5      .   (net.PaulChristensen.TestRunnerW32.Runner   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      9System&clr-namespace:System;assembly=mscorlib     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Q
properties:clr-namespace:net.PaulChristensen.TestRunnerW32.Properties     5      <my-http://schemas.microsoft.com/wpf/2008/toolkit     5	      -   5          �Name$  MainTestRunner��5
      $	��427��5      $
��1138��5       � Title$ Main Test Runner��5      .��5      � -   5         �Name$ BaseWindowGrid��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   $��20��6*   �� 5      $��70��6   �� 5      $
��Auto �6   $��5��6*   �� 5      $��70��6   �� 5      $
��Auto �6   $��20��6*    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   $	��120��6,   �� 5      $��* �6   .+�5   
   o� -   5        $��MainMenu��5      $��20��6   $	��Top=�6+   $��2��6C   $��0,0,0,0q�6W   �� 5   
   -   5        $��StackPanelLeft��5   0   $
��Left=�6   $	��120��5      $��1��6!   $��3��6.   .+�5      �� -   5        $��ClearQueButton��5   !   $��28��6   $
��Auto��67   ;  5net.PaulChristensen.TestRunnerW32.Properties.Resource   ClearTestQueButtonText#��Z 5      �� 5      -   5        $��ExecuteTestsButton��5   !   $��28��6   $
��Auto��6;      ExecuteTestsButtonText#��Z 5      �� 5      -   5        $��SaveSuiteButton��5   !   $��28��6   $
��Auto��68   $��$Save Suite$��5       �� 5!      -   5        $��QueAllTests��5!   !   $��28��6   $
��Auto��64   $��$QueAllTests$��5"      5#      �� 5$   
   -	   5        $��HorizontalGridSplitter��5$   Q   $��1��6   $��Stretch=�6(   $��0q�6F    �� ResizeDirection$
 Rows=�5%      $��2��6.   $��5��6;   $��2��6F   � 5&   
   -
   5        $��TopGrid��5&      $��1��6   $��1��6+   $��0,-1,0,0q�6;   $��2��6M    5'      �� 5(      $��* �6#   �� 5)      $
��Auto �6#   �� 5*      $��* �6#   .+�5,      �� -   5        $��VerticalGridSplitter��5,   D   $��Stretch=�6   $��0q�69   $��3��6`   $ Columns=�5-      $��1��65   $��Stretch=�6E   �� 5.      -   5         ��Name$ AvailableTests��5.      $	��1,1q�6/   $	 �� IsSynchronizedWithCurrentItem$
	 TrueY�6<   
 J� 	AllowDrop
 .  �� SelectionMode$ Single=�50   [   $��0��51       �� View 52      �� 53      .P�54      �� 
 X� NaN �� Width# Z 6)      TestNameColumnHeaderText#��Z 55   )    �� DisplayMemberBinding 56   )   �� *
TestName+�� 57      # Z 6)   &   TestDescriptionColumnHeaderText#��Z 58   )    59   )   �� *TestDescription+5:      5<      �� 5=      -   5        $ ExecutionList��5=      $	��1,1q�6.   $
	 TrueY�6;   
 . $��2��5?   (    5@      �� 5A      .P�5B      �� # Z 6)   #��Z 5C   )    5D   )   �� *
TestName+�� 5E      # Z 6)   #��Z 5F   )    5G   )   �� *TestDescription+5H      5J      5K      �� 5L   
   -   5        $��ResultsView��5L   %   $��1,1,1,1q�6   $
	 TrueY�68    �� AlternationCount$ 1��6]   $��3��5M      $��1��6    $
��Auto��60    5N      �� 5O      .P�5P      �� # Z 6%      TestIdColumnHeaderText#��Z 6J    5Q   %   �� *CurrentTestId+�� 5R      $	 120��6%   #��Z 61    5S   %   �� *CurrentTestName+�� 5T      $	 200��6%   !   TestStatusColumnHeaderText#��Z 61    5U   %   �� *CurrentTestStatus+�� 5V      $	 300��6%   "   TestResultsColumnHeaderText#��Z 61    5W   %   �� *CurrentTestResults+5X      5Z      �� 5[   
   -   5        $��	StatusBar��5[      $��20��6%   $	��Top=�61   $��4��6I   $��2��6V   .@�5\      ��    TotalTestCountStatusText#��Z 6   �� 5]      ��6   ��  �� Path$ TestCountString��� 5^      �� 5_      $��$Run Test Count$:��6   �� 5`      ��6   �� $ RunTestCountString��� 5a      $   ExecutingTestNumberStatusText#��Z 6   �� 5b      ��6   �� $ CurrentTestCountString��� 5c      $
�� Of ��6   �� 5d      ��6   �� $ RunTestCountString��� 5e      �� 5f      $��Stretch=�6   $��Stretch=�6A   .��5g      � -   5        $��ProgSuiteProgress��5g       � Value 67   �� *SuiteProgressPercent+ �� UpdateSourceTrigger$ PropertyChanged=�5h      5i      5j      5k      