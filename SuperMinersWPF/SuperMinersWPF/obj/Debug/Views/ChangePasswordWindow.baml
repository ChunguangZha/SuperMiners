   M S B A M L   `   `   `  ����  SuperMinersWPF/    )SuperMinersWPF.Views.ChangePasswordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  修改密码��5   	   $	��200��6   $	��300��6#    � WindowStartupLocation$ CenterScreen=�5   	      PlayerInfoBackground#���   6.   
������� � WindowStyle$ 
ToolWindow=�6u   .��5      �  � ColumnDefinitions 5	   
   �� 5
      $
��103* �6   �� 5      $
��189* �6    � RowDefinitions 5   
   �� 5      $	��52* �6   �� 5      $	��51* �6   �� 5      $	��52* �6   �� 5      $
��114* �6   .+�5   
   �� $��原密码：��6   $��5q�6    $��Right=�6+   $��Center=�6G   G� 5   
   -   5        $��txtOldPassword��5   &   $��1��6   $��5q�6<    G� 	MaxLength$ 30��6G   $��Center=�6V   �� 5   
   $��1��6   $��新密码：��6!   $��5q�6-   $��Right=�68   $��Center=�6T   G� 5   
   -   5        $��txtNewPassword��5   3   $��1��6   $��1��6#   $ 30��6I   $��5q�6X   $��Center=�6c   �� 5   
   $��2��6   $��确认新密码：��6!   $��5q�6/   $��Right=�6:   $��Center=�6V   G� 5   
   -   5        $��txtConfirmNewPassword��5   3   $��2��6   $��1��6#   $ 30��6P   $��5q�6_   $��Center=�6j   �� 5   
   -   5        $��	btnCancel��5   2   $��3��6   $��2��6   $��取消��6C   $��75��6P   $��Right=�6[   $��Center=�6w   $��10q�6�   6�   �� 5   
   -   5        $��btnOK��5   2   $��3��6   $��2��6   $��确定��6?   $��75��6L   $��Right=�6W   $��Center=�6s   $��10,10,100,10q�6�   6�   5      5      