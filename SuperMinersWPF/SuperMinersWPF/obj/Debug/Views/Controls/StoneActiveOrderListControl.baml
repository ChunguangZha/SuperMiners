   M S B A M L   `   `   `  ����I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5      ;"clr-namespace:SuperMinersWPF.ViewsSuperMinersWPF.Views  5      ?    9SuperMinersWPF.Views.Controls.StoneActiveOrderListControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      5my&clr-namespace:SuperMinersWPF.MyControl     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:SuperMinersWPF.Views     5      -   5        .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $	��96* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��btnRefreshOrderList��5      $��刷新��6,   $	��100��69   $��30,10q�6E   $
��Left=�6T   6o   �� 5   
   -   5         ��Name$ listboxAllSellOrders��5      $��1��6   $��Stretch=�6=   $��5q�6b   ��5      �� 5      .R�5      �  � ColumnDefinitions 5      �� 5      $��35 �6/   �� 5      $	��150 �6/   �� 5      $
��Auto �6/   �� 5      $
��Auto �6/   �� 5      $
��Auto �6/   �� 5      $
��Auto �6/   �� 5      $
��Auto �6/   �� 5      $
��Auto �6/   �� 5      $
��Auto �6/   �� 5       �� 5!      $
��Auto �6/   .+�5#      �� $��0��6%   $��2��65    �� Orientation$ 
Horizontal=�6I   .+�5$      �� 
����  �$��Italic$�6^   $��5q�6q   $
��Left=�6|   ��6(   �� *SellerUserName+6|   �� 5%      $��Center=�6I   $��Right=�6d   $��16��6�   $��Uniform=�6�   ��6$   �� *SellerExpLevelImg+6�   �� 5&      $��Center=�6L   $��Right=�6g   $��16��6�   $��Uniform=�6�   ��6$   �� *SellerCreditLevelImg+6�   5'      �� 5(      $��2��6$   $��挂单出售��64   $��5q�6@   �� 5)      $��3��6$   
���  ��$��Italic$�6f   $��5q�6y   ��64   �� *SellStonesCount+6y   �� 5*      $��4��6$   $��5q�6�   ��64   �� *Strings.Stone+ �� Mode$ OneWay=�   Language �� Source# [  6�   �� 5+      $��5��6$   $��价值��64   $��20,5,5,5q�6>   �� 5,      $��6��6$   
���  ��$��Italic$�6_   $��5q�6r   ��64   �� *
ValueRMB+6r   �� 5-      $��7��6$   $��5q�6�   ��64   �� *Strings.RMB+$ OneWay=�# [  6�   �� 5.      $��8��6$   $��5q�6}   ��64   �� *OrderStateString+��6V   �� *OrderStateBrush+6}   �� 5/      $��9��6$   
����  �$��Right=�6`   $��5q�6|   ��64   �� *
SellTime+6|   �� 50      -   5        $��10��50   !   $��接单��62   $��50��6e   $��5q�6p   {�6?   �� *BuyButtonEnable+6{   51      52      54      55      56      