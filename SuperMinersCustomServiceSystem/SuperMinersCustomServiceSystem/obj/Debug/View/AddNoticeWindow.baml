   M S B A M L   `   `   `  ����"  SuperMinersCustomServiceSystem9    3SuperMinersCustomServiceSystem.View.AddNoticeWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  添加通知��5   	   $	��600��6   $	��600��6#   $��16(�6/   .��5      � $��5q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��标题��6   $��10q�6   $��Right=�6*   $��Center=�6F   �� 5   
   -   5        $��txtNoticeTitle��5   "   $��1��6   $��5q�68   $��Center=�6C   �� 5   
   $��1��6   $��内容��6!   $��10q�6+   $��Right=�67   $	��Top=�6S   �� 5   
   -   5        $��txtNoticeContent��5   /   $��1��6   $��1��6   $��5q�6G   $ �� HorizontalScrollBarVisibility$ Disabled=�6R   " �� VerticalScrollBarVisibility$
 Auto=�6{    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn .  �� 
AcceptsTab . 6�   �� 5   
   -   5        $��btnOK��5   2   $��2��6   $��3��6   $��确定��6?   $��75��6L   $��	5,5,110,5q�6W   $��Right=�6j   $	��Top=�6�   6�   �� 5   
   -   5        $��	btnCancel��5   2   $��2��6   $��3��6   $��取消��6C   $��75��6P   $��5q�6[   $��Right=�6f   $	��Top=�6�   6�   5      5      