   M S B A M L   `   `   `  ����I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5   	   G    ASuperMinersWPF.Views.Windows.ViewGambleStoneUserBetInRecordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   <	MyControl&clr-namespace:SuperMinersWPF.MyControl     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  疯狂猜石下注记录��5   	      PlayerInfoBackground#���   5   	   
�������$	��500��5   	   $	��700��6    � WindowStartupLocation$ CenterScreen=�6"   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��3��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� -   5        $��	btnSearch��5      $��查询��6&   $	��100��63   $��10q�6?   $��Center=�6K   6f   �� 5      $��每页显示：30 行��6   $��50,10,10,10q�6)   $��Center=�6>   �� 5      -   5        $��btnPreviousPage��5      $��	上一页��6,   $��70��6:   $��10q�6E   $��Center=�6Q   6l   �� 5      $��当前显示第��6   $
��5,10q�6%   $��Center=�63   -   'SuperMinersWPF.MyControl.NumericTextBox  5      -   5          Name$ numPageIndex��5   '   $��50��6=    �� 
IsReadOnly . $
��5,10q�6]   $��Center=�6k      Minimum$ 1Q�6�      Maximum$ 10000000Q�6�   �� 5      $	��页��6   $
��5,10q�6!   $��Center=�6/   �� 5      -   5        $��btnNextPage��5      $��	下一页��6(   $��70��66   $��10q�6A   $��Center=�6M   6h   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��datagrid��5       $��4��6      AutoGenerateColumns .  	   
IsReadOnly	 . 
   SelectionMode$
 Single=�6^      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6%      Header$ 所中奖项��60   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *AwardItemName+  5      $ * 6%   $ 中奖时间��60    6>   �� *	WinTime+  5      $ * 6%   $ 领奖QQ��60    6>   �� *
GotInfo1+  5       $ * 6%   $ 领奖电话��60    6>   �� *
GotInfo2+  5!      $ * 6%   $ 是否领取��60    6>   �� *IsGot+  5"      $ * 6%   $ 领取时间��60    6>   �� *	GotTime+  5#      $ * 6%   $ 是否支付��60    6>   �� *IsPay+  5$      $ * 6%   $ 支付时间��60    6>   �� *	PayTime+5&      5'      5(      