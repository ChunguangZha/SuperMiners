   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  +    %SuperMinersWPF.Views.SellStonesWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5my&clr-namespace:SuperMinersWPF.MyControl     -     � Title$  出售矿石��$	��330��$	��300�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�   PlayerInfoBackground#���   
������� � WindowStyle$ 
ToolWindow=�.��� $��5q� � ColumnDefinitions �� $
��Auto ��� $	��97* ��� $
��Auto � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto �.+��� $��	总库存��$��Right=�$��5q��� -   $��txtAllStones��$��1�� �� 
IsReadOnly . $��5q��� $��2��$��5q����� *Strings.Stone+ �� Mode$ OneWay=�  Language �� Source# [ �� $��1��$��	已冻结��$��Right=�$��5q��� -   $��txtForzenStones��$��1��$��1�� . $��5q��� $��1��$��2��$��5q����� *Strings.Stone+$ OneWay=�# [ �� $��2��$��	可出售��$��Right=�$��5q��� -   $��txtSellableStones��$��2��$��1�� . $��5q��� $��2��$��2��$��5q����� *Strings.Stone+$ OneWay=�# [ �� $��3��$��本次出售��$��Right=�$��5q�-   'SuperMinersWPF.MyControl.NumericTextBox  -   	  Name$	 numSellStones��-   'SuperMinersWPF.MyControl.NumericTextBox
   ValueChanged$ 
 numSellStones_ValueChanged��$��3��$��1��   Minimum$ 0Q�   Maximum$ 100000Q�$��5q��� $��3��$��2��$��5q����� *Strings.Stone+$ OneWay=�# [ �� $��4��$��	可获得��$��Right=�$��5q��� -   $��	txtGetRMB��$��4��$��1�� . 
��� � �$��5q��� $��4��$��2��$��5q����� *Strings.RMB+$ OneWay=�# [ �� $��5��$��	手续费��$��Right=�$��5q��� -   $��
txtExpense��$��5��$��1�� . 
��� � �$��5q��� $��5��$��2��$��5q����� *Strings.RMB+$ OneWay=�# [ �� $��6��$��0��$��提示��$��Right=�$	��Top=�$��5q��� $��6��$��1��$��5q�.+��� $
��Wrap=�
���  ���� $�出售矿石需要扣除�� �� -    �Name$ subTxtExpensePercent��$� �� �� $�%的手续费。���� $
��Wrap=�
���  ���� $�手续费最低为�� �� -	   $ subTxtMinExpense��$� �� �� ��� *Strings.RMB+$ OneWay=�# [ �� -
   $��	btnCancel��$��7��$��3��$��取消��$��75��$��Right=�$��5q��� -   $��btnSell��$��7��$��3��$��出售��$��75��$��Right=�$��	5,5,110,5q�