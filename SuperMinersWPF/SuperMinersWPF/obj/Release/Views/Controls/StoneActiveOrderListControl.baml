   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  ;"clr-namespace:SuperMinersWPF.ViewsSuperMinersWPF.Views  ?    9SuperMinersWPF.Views.Controls.StoneActiveOrderListControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5my&clr-namespace:SuperMinersWPF.MyControl     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:SuperMinersWPF.Views     -   .���   � RowDefinitions  �� $
��Auto ��� $	��96* ��� $
��Auto �.+��� -   $��btnRefreshOrderList��$��刷新��$	��100��$��30,10q�$
��Left=��� -    ��Name$ listboxAllSellOrders��$��1��$��Stretch=�$��5q����� .R��  � ColumnDefinitions �� $��35 ��� $	��150 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto �.+��� $��0��$��2�� �� Orientation$ 
Horizontal=�.+��� 
����  �$��Italic$�$��5q�$
��Left=����� *SellerUserName+�� $��Center=�$��Right=�$��16��$��Uniform=����� *SellerExpLevelImg+�� $��Center=�$��Right=�$��16��$��Uniform=����� *SellerCreditLevelImg+�� $��2��$��挂单出售��$��5q��� $��3��
���  ��$��Italic$�$��5q����� *SellStonesCount+�� $��4��$��5q����� *Strings.Stone+ �� Mode$ OneWay=�   Language �� Source# [  �� $��5��$��价值��$��20,5,5,5q��� $��6��
���  ��$��Italic$�$��5q����� *
ValueRMB+�� $��7��$��5q����� *Strings.RMB+$ OneWay=�# [  �� $��8��$��5q����� *OrderStateString+���� *OrderStateBrush+�� $��9��
����  �$��Right=�$��5q����� *
SellTime+�� -   $��10��$��接单��$��50��$��5q�{��� *BuyButtonEnable+