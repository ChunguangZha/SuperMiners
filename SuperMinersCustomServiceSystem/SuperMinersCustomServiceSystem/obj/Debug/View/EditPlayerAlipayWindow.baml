   M S B A M L   `   `   `  ����"  SuperMinersCustomServiceSystem@    :SuperMinersCustomServiceSystem.View.EditPlayerAlipayWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$!  修改玩家支付宝信息��5   	   $	��250��6   $	��300��6(    � 
ResizeMode$ NoResize=�64    � WindowStartupLocation$ CenterScreen=�6J   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5       � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��用户名：��6   $��5q�6    $��Right=�6+   �� 5   
   -   5        $��txtUserName��5   "   $��1��6    �� 
IsReadOnly . $��5q�6G   �� 5   
   $��1��6   $��支付宝账户：��6!   $��5q�6/   $��Right=�6:   �� 5   
   -   5        $��txtAlipayAccount��5   /   $��1��6   $��1��6   $��5q�6G    �� 	MaxLength$ 30��6R   �� 5   
   $��2��6   $��支付宝真实姓名：��6!   $��5q�61   $��Right=�6<   �� 5   
   -   5        $��txtAlipayRealName��5   /   $��2��6   $��1��6   $��5q�6H   $ 10��6S   �� 5   
   $��3��6   $��身份证号：��6!   $��5q�6.   $��Right=�69   �� 5   
   -   5        $��txtIDCardNo��5   /   $��3��6   $��1��6   $��5q�6B   $ 18��6M   �� 5   
   -   5        $��btnOK��5   2   $��5��6   $��2��6   $��确定��6?   $	��100��6L   $��Right=�6X   $��Bottom=�6t   $��	5,5,120,5q�6�   6�   �� 5   
   -   5        $��	btnCancel��5   2   $��5��6   $��2��6   $��取消��6C   $	��100��6P   $��Right=�6\   $��Bottom=�6x   $��5q�6�   6�   5      5      