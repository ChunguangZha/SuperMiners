   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  G    ASuperMinersWPF.Views.Windows.ViewGambleStoneUserBetInRecordWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     <	MyControl&clr-namespace:SuperMinersWPF.MyControl     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  疯狂猜石下注记录��   PlayerInfoBackground#���   
�������$	��500��$	��700�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��3�� �� Orientation$ 
Horizontal=�.+��� -   $��	btnSearch��$��查询��$	��100��$��10q�$��Center=��� $��每页显示：30 行��$��50,10,10,10q�$��Center=��� -   $��btnPreviousPage��$��	上一页��$��70��$��10q�$��Center=��� $��当前显示第��$
��5,10q�$��Center=�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numPageIndex��$��50�� �� 
IsReadOnly . $
��5,10q�$��Center=�   Minimum$ 1Q�   Maximum$ 10000000Q��� $	��页��$
��5,10q�$��Center=��� -   $��btnNextPage��$��	下一页��$��70��$��10q�$��Center=�&   System.Windows.Controls.DataGrid  -   $��datagrid��$��4��   AutoGenerateColumns .  	   
IsReadOnly	 . 
   SelectionMode$
 Single=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    Header$ 所中奖项��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *AwardItemName+  $ * $ 中奖时间�� �� *	WinTime+  $ * $ 领奖QQ�� �� *
GotInfo1+  $ * $ 领奖电话�� �� *
GotInfo2+  $ * $ 是否领取�� �� *IsGot+  $ * $ 领取时间�� �� *	GotTime+  $ * $ 是否支付�� �� *IsPay+  $ * $ 支付时间�� �� *	PayTime+