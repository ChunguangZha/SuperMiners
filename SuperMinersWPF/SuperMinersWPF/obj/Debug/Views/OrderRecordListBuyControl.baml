   M S B A M L   `   `   `  ����  SuperMinersWPF4    .SuperMinersWPF.Views.OrderRecordListBuyControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        .��5	      �   � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��5q�6.   .+�5      �� $	��共��6   �� 5      -   5        $��txtItemsCount��5   !   $��0��6   $
��10,0q�66   �� 5      $��	条记录��6   5      �� 5   
   -   5        $��listboxBuyOrder��5      $��1��6   $��Stretch=�66   ��5      �� 5      .R�5      � $��5q�6     5      �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      �� 5      $
��Auto �6,   .+�5      �� $ 
Horizontal=�6%   .+�5      �� $��订单号：��6(   $��14(�64   $��5q�6B   �� 5      $��14(�6E   $
��Bold"�6S   $��5q�6e   ��6(   �� *OrderNumber+6e   5       �� 5!      $��1��6%   $ 
Horizontal=�62   .+�5"      �� $��买入矿石：��6(   $��5q�65   �� 5#      
���  ��$��Italic$�6Z   $��5q�6m   ��6(   �� *SellStonesCount+6m   �� 5$      $��支付灵币：��6(   $��30,5,5,5q�65   �� 5%      
���  ��$��Italic$�6S   $��5q�6f   ��6(   �� *
ValueRMB+6f   �� 5&      $��获取金币奖励：��6(   $��30,5,5,5q�67   �� 5'      
���  ��$��Italic$�6X   $��5q�6k   ��6(   �� *AwardGoldCoin+6k   �� 5(      $��买入时间：��6(   $��30,5,5,5q�65   �� 5)      
���  ��$��Italic$�6R   $��5q�6e   ��6(   �� *	BuyTime+6e   5*      �� 5+      $��2��6%   $ 
Horizontal=�62   .+�5,      �� $��	卖家：��6(   $��5q�63   �� 5-      
���  ��$��Italic$�6Y   $��5q�6l   ��6(   �� *SellerUserName+6l   �� 5.      $��挂单时间：��6(   $��30,5,5,5q�65   �� 5/      
���  ��$��Italic$�6S   $��5q�6f   ��6(   �� *
SellTime+6f   50      �� 51      $��3��6    �� StrokeDashArray$	 4,2R�6,   ���	#FF8D8D8D	 �� X2$	 300��6S   52      53      55      56      57      