   M S B A M L   `   `   `  ����"  SuperMinersCustomServiceSystemR    LSuperMinersCustomServiceSystem.View.Windows.InputUserLoginNamePasswordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	请输入��5   	   $	��300��6   $	��300��6"    � WindowStyle$ 
ToolWindow=�6.    � WindowStartupLocation$ CenterScreen=�6G   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5	      �� 5
       � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      .+�5   
   �� $��2��6   $����需要输入玩家新区账户和密码。如果没有，则会按原用户名创建，如果原（用户名、昵称）被使用，则无法转区��6(   $
��Wrap=�6a   $��10q�6u   �� 5   
   $��1��6   $��新区用户名：��6!   $��5q�6/   $��Center=�6:   $��Right=�6U   �� 5   
   -   5        $��txtNewServerUserLoginName��5   /   $��1��6   $��1��6   $��5q�6P   $��Center=�6[   �� 5   
   $��2��6   $��新区密码：��6!   $��5q�6.   $��Center=�69   $��Right=�6T   G� 5   
   -   5        $��txtNewServerPassword��5   3   $��2��6   $��1��6#   $��5q�6O   $��Center=�6Z   �� 5   
   -   5        $��	btnCancel��5   2   $��3��6   $��2��6   $��取消��6C   $
��10,5q�6P   $	��100��6^   $��Center=�6j   $��Right=�6�   6�   �� 5   
   -   5        $��btnOK��5   2   $��3��6   $��2��6   $��确定��6?   $��
10,5,120,5q�6L   $	��100��6`   $��Center=�6l   $��Right=�6�   6�   5      5      