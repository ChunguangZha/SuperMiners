   M S B A M L   `   `   `  ����Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5   	   J    DSuperMinersCustomServiceSystem.View.Windows.EditPlayerDiamondsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   <	MyControl&clr-namespace:SuperMinersWPF.MyControl     5   	     � Title$  修改玩家钻石��5   	   $	��300��6   $	��400��6%    � 
ResizeMode$ NoResize=�61    � WindowStartupLocation$ CenterScreen=�6G   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $	��150 �6   �� 5	       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��	玩家：��6   $��5q�6   $��Right=�6*   �� 5   
   -   5        $��txtUserName��5   "   $��1��6    �� 
IsReadOnly . $��5q�6G   �� 5   
   $��1��6   $��0��6!   $��现有可用钻石：��61   $��5q�6@   $��Right=�6K   �� 5   
   -   5        $��txtCurrentStackDiamonds��5   /   $��1��6   $��1��6    . $��5q�6`   �� 5   
   $��2��6   $��修改后可用钻石：��6!   $��5q�61   $��Right=�6<   -   'SuperMinersWPF.MyControl.NumericTextBox  5   
   -   5          Name$ txtStackDiamondsChanged��5   @   $��2��6#   $��1��60      Minimum$ 0Q�6a      Maximum$ 200000Q�6m   $��5q�6~   	   DecimalPlaces$	 2��6�   �� 5   
   $��3��6   $��0��6!   $��现有冻结钻石：��61   $��5q�6@   $��Right=�6K   �� 5   
   -   5        $ ��txtCurrentFreezingDiamonds��5   /   $��3��6   $��1��6    . $��5q�6c   �� 5   
   $��4��6   $��修改后冻结钻石：��6!   $��5q�61   $��Right=�6<     5   
   -   5        $  txtFreezingDiamondsChanged��5   @   $��4��6#   $��1��60   $ 0Q�6d   $ 200000Q�6p   $��5q�6�   $	 2��6�   �� 5   
   -   5        $��btnOK��5   2   $��5��6   $��2��6   $��确定��6?   $	��100��6L   $��Right=�6X   $��Bottom=�6t   $��	5,5,120,5q�6�   6�   �� 5   
   -   5        $��	btnCancel��5   2   $��5��6   $��2��6   $��取消��6C   $	��100��6P   $��Right=�6\   $��Bottom=�6x   $��5q�6�   6�   5      5       