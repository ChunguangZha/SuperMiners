   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  M+clr-namespace:SuperMinersWPF.Views.ControlsSuperMinersWPF.Views.Controls  -    'SuperMinersWPF.Views.StackMarketControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5my&clr-namespace:SuperMinersWPF.MyControl     =views+clr-namespace:SuperMinersWPF.Views.Controls     -     � 	Resources   �� 
TargetType �� Property   LogoPanelBackground �� Value��%B   '��       7   �� " ��.���  � ��8   ��  � ��$ 1q�.���  � ColumnDefinitions �� $��* ��� $	��200 � � RowDefinitions �� $
��Auto ��� .+�� $��10q�
���   � �� $
��Auto ��� .+��  �� �� �� �� �� ��  �� �� .+��� $��2��$��22(�$��Center=�$	��5,0q����� *TodayStackInfo.ClosePrice+ �� Mode$ OneWay=����� * TodayStackInfo.ClosePriceColor+�� $��1��$��0��$��Bottom=����� *TodayStackInfo.RiseValueColor+�� ��� *TodayStackInfo.RisePercent+$ OneWay=� �� $�%���� $��1��$��1��$��Bottom=����� *TodayStackInfo.RiseValue+$ OneWay=����� *TodayStackInfo.RiseValueColor+�� $��2��$��2��
�������$��Center=��� $
�开 �� �� ��� *TodayStackInfo.OpenPrice+$ OneWay=��� $��3���� $
�高 ��
������� �� ��� *%#TodayStackInfo.MaxTradeSucceedPrice+$ OneWay=����� **(TodayStackInfo.MaxTradeSucceedPriceColor+�� $��3��$��1���� $
�低 ��
������� �� ��� *%#TodayStackInfo.MinTradeSucceedPrice+$ OneWay=����� **(TodayStackInfo.MinTradeSucceedPriceColor+�� $��4��
��������� $�涨停 �� �� 
���  ����� *TodayStackInfo.LimitUpPrice+$ OneWay=��� $��4��$��1��
��������� $�跌停 �� �� 
��� � ���� *TodayStackInfo.LimitDownPrice+$ OneWay=��� $��5��
��������� $
�量 �� �� ��� *)'TodayStackInfo.TradeSucceedStoneHandSum+$ OneWay=��� $��5��$��1��
��������� $
�额 �� �� ��� *#!TodayStackInfo.TradeSucceedRMBSum+$ OneWay=�0   *SuperMinersWPF.Views.Controls.KLineControl  $��1���� $��1��$��10q�$
��Auto��$	��Top=�.+�� $��Stretch=� �� $��1* ��� $��2* ��� $��2* �.+��� $��2��
�������$��1q�._��� $��每手价格��$
��10,5q�$��Center=�$��Center=��� $��2��
�������$��1q�._��� $��数量(手)��$
��10,5q�$��Center=�$��Center=��� -   $��lvSell5Price��$��Stretch=����� *" TodayStackInfo.Top5SellOrderList+���� .R�� $	��0,2q� �� $��1* ��� $��2* ��� $��2* �.+��� $��0��
���  ������ *Title+�� $��1��
���  ������ *Price+�� $��2��
���  ������ *Count+�� -   $��lvBuy5Price��$��Stretch=����� *!TodayStackInfo.Top5BuyOrderList+���� .R�� $	��0,2q� �� $��1* ��� $��2* ��� $��2* �.+��� $��0��
��� � ����� *Title+�� $��1��
��� � ����� *Price+�� $��2��
��� � ����� *Count+� $��1��$��10q� �� $
��Auto ��� .+��� $��说明��$��18(�$��Center=�$��5q��� $��1��$��10q�  RedButtonBackground#��[ ._��� $����       市场以灵币作为流通货币，以矿石作为交易对象。以一千矿石为最小交易单位，称为一手。矿石的价格也以一手矿石来计算，矿石价格的最小单位是0.01灵币。��
�������$��18(�$��10q�$
��Wrap=��� -   $��tabTradeOper��$��1��$��1��$��10q� �� TabStripPlacement$ Bottom=�.@��� $��购买矿石��.��� $��10q� �� $��* ��� $��2* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��1��$��矿石��$��Center=�$��Right=�$
��10,5q�-   'SuperMinersWPF.MyControl.NumericTextBox  -     Name$ numStoneHandCount��$��1��$��2��	   Value$	 1Q�
   Minimum$
 1Q�   Maximum$ 10000Q�$
��10,5q��� $��2��$��价格��$��Center=�$��Right=�$
��10,5q��� -    ��Name$ numPrice��$��2��$��2�� �� 
IsReadOnly . $
��10,5q��� -   $��sliderPrice��$��3��$��1�� � Value$	 100Q� � Minimum$	 100Q� � Maximum$	 110Q� � SmallChange$
 0.01Q�$��5q� � LargeChange$
 0.01Q��� $��4��$��0��$��支付��$��Center=�$��Right=�$
��10,5q��� -   $��
cmbPayType��$��4��$��1�� �� SelectedValuePath$	 Key�� �� DisplayMemberPath$ Value��$��Center=�$��Stretch=�$
��10,5q� �� SelectedIndex$ 0���� -	   $��btnBuyStone��$��5��$��2��$��挂单购买��  UserInfoButtonStyle#��[ $	��100��$��30��$
��10,5q�$��Center=�$��Center=��� $��买单��.���� -
   $��lvNotFinishedBuyOrder��$��Stretch=����� *AllNotFinishedBuyOrders+���� .R��� .���  �� $
��Auto ���  �� �� �� �� �� �� $
��Auto �.+��� $��2��$��5q����� *DelegateTimeText+$ OneWay=��� $��1��$��交易量：��$��5q��� $��1��$��1��
���  ��$��5q��� ��� *TradeStoneHandCount+$ OneWay=� �� $� 手 ���� $��2��$��
 价格：��$��5q��� $��2��$��1��
���  ��$��5q����� *Price+�� $��3��$��
 状态：��$��5q��� $��3��$��1��
���  ��$��5q����� *BuyStateText+�� -   $��4��$��2��$��撤消��$	��100��$
��10,5q��� -   $��5��$��2��$��支付��$	��100��$
��10,5q�y��� *PayButtonVisible+�� $��卖单��.���� -   $��lvNotFinishedSellOrder��$��Stretch=����� *AllNotFinishedSellOrders+���� .R��� .���  �� $
��Auto ���  �� �� �� �� �� $
��Auto �.+��� $��2��$��5q����� *CreateTimeText+�� $��1��$��0��$��交易量：��$��5q��� $��1��$��1��
���  ��$��5q��� ��� *TradeStoneHandCount+$ OneWay=� �� $� 手 ���� $��2��$��0��$��
 价格：��$��5q��� $��2��$��1��
���  ��$��5q����� *Price+�� $��3��$��0��$��
 状态：��$��5q��� $��3��$��1��
���  ��$��5q����� *SellStateText+�� -   $��4��$��2��$��撤消��$	��100��$
��10,5q�