   M S B A M L   `   `   `  ����  SuperMinersWPF-    'SuperMinersWPF.Views.GatherStonesWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  收取矿石��5   	   $	��230��6   $	��300��6#      PlayerInfoBackground#���   5   	   
������� � WindowStartupLocation$ CenterScreen=�6P    � 
ResizeMode$ NoResize=�6u    � WindowStyle$ 
ToolWindow=�6�   .��5      � $��5q�6    � ColumnDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��当前生产��6   $��5q�6    $��Right=�6+   $��Center=�6G   �� 5   
   -   5        $��txtAllOutputStones��5   "   $��1��6    �� 
IsReadOnly . $��5q�6N   $��Center=�6Y   �� 5   
   $��2��6   $��Center=�6r   $��5q�6�   ��6$   �� *Strings.Stone+ �� Mode$ OneWay=�  Language �� Source# [ 6�   �� 5   
   $��1��6   $��	可收取��6!   $��5q�6,   $��Right=�67   $��Center=�6S   �� 5   
   -   5        $��txtGatherableStones��5   /   $��1��6   $��1��6    . $��5q�6\   $��Center=�6g   �� 5   
   $��1��6   $��2��6!   $��Center=�6   $��5q�6�   ��61   �� *Strings.Stone+$ OneWay=�# [ 6�   �� 5   
   $��2��6   $��提示��6!   $��5q�6+   $��Right=�66   �� 5   
   $��2��6   $��1��6!   $]��W系统只按整数进行收取，小数部分将自动舍去，请确认是否收取？��61   $��5q�6V   $
��Wrap=�6a   �� 5   
   -   5        $��btnOK��5   2   $��3��6   $��3��6   $��收取��6?   $��75��6L   $��	5,5,110,5q�6W   $��Right=�6j   $	��Top=�6�   6�   �� 5   
   -   5        $��	btnCancel��5   2   $��3��6   $��3��6   $��取消��6C   $��75��6P   $��5q�6[   $��Right=�6f   $	��Top=�6�   6�   5      5      