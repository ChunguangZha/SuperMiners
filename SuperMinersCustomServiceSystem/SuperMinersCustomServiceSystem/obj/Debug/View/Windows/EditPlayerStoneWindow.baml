   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5   	   G    ASuperMinersCustomServiceSystem.View.Windows.EditPlayerStoneWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   :	MyControl&clr-namespace:SuperMinersWPF.MyControl    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  修改玩家矿石信息��5   	   $	��300��6   $	��400��6'    � WindowStyle$ 
ToolWindow=�63    � WindowStartupLocation$ CenterScreen=�6L   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $	��53* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��总矿石储量��6   $��5q�6!   $��Right=�6,   �� 5   
   -   5        $��txtCurrentStonesReserves��5   "   $��1��6    �� 
IsReadOnly . $��5q�6T   �� 5   
   $��2��6   $��	修改为��6$   $��5q�6/   $��Right=�6:   -   'SuperMinersWPF.MyControl.NumericTextBox  5   
   -   5          Name$ numChangedStonesReserves��5   3   $��3��6#      DecimalPlaces$ 2��6U      Minimum$ 0Q�6g   	   Maximum$	 10000000000Q�6s   $��5q�6�   �� 5   
   $��2��6   $��库存矿石量��6!   $��5q�6.   $��Right=�69   �� 5   
   -   5        $��txtCurrentStockOfStones��5   /   $��2��6   $��1��6    . $��5q�6`   �� 5   
   $��2��6   $��2��6!   $��	修改为��61   $��5q�6<   $��Right=�6G     5   
   -   5        $ numChangedStockOfStones��5   @   $��2��6#   $��3��60   $ 2��6a   $ 0Q�6s   $	 10000000000Q�6   $��5q�6�   �� 5   
   $��3��6   $��冻结矿石量��6!   $��5q�6.   $��Right=�69   �� 5    
   -   5        $��txtCurrentFreezingStones��5    /   $��3��6   $��1��6    . $��5q�6a   �� 5!   
   $��3��6   $��2��6!   $��	修改为��61   $��5q�6<   $��Right=�6G     5"   
   -   5        $ numChangedFreezingStones��5"   @   $��3��6#   $��3��60   $ 2��6b   $ 0Q�6t   $	 10000000000Q�6�   $��5q�6�   �� 5#   
   $��4��6   $��可出售矿石量��6!   $��5q�6/   $��Right=�6:   �� 5$   
   -   5        $��txtCurrentSellableStones��5$   /   $��4��6   $��1��6    . $��5q�6a   �� 5%   
   $��4��6   $��2��6!   $��	修改为��61   $��5q�6<   $��Right=�6G     5&   
   -   5        $ numChangedSellableStones��5&   @   $��4��6#   $��3��60    . $ 2��6t   $ 0Q�6�   $	 10000000000Q�6�   $��5q�6�   �� 5(   
   -	   5        $��btnOK��5(   2   $��7��6   $��4��6   $��确定��6?   $	��100��6L   $��10,10,120,10q�6X   $��Right=�6n   6�   �� 5)   
   -
   5        $��	btnCancel��5)   2   $��7��6   $��4��6   $��取消��6C   $	��100��6P   $��10q�6\   $��Right=�6h   6�   5*      5+      