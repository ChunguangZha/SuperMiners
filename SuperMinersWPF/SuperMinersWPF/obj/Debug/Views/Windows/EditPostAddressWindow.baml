   M S B A M L   `   `   `  ����  SuperMinersWPF8    2SuperMinersWPF.Views.Windows.EditPostAddressWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  EditPostAddressWindow��5   	   $	��300��6'   $	��600��64    � WindowStartupLocation$ CenterScreen=�6@    � WindowStyle$ 
ToolWindow=�6e   .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5	      �� 5
      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��收件人：��6   $��Center=�6    $
��Left=�6;   $��10q�6V   �� 5   
   $��1��6   $��2��6"    �� 	MaxLength$ 50��6T   $��Center=�6c   ��66   �� *ReceiverName+6c   �� 5   
   $��3��6   $��联系电话：��6$   $��Center=�61   $��Center=�6L   $��10q�6i   �� 5   
   $��4��6   $��3��6"   $ 12��6S   $��Center=�6b   ��66   �� *PhoneNumber+6b   �� 5   
   $��1��6   $��收件地址：��6!   $��Center=�6.   $
��Left=�6I   $��10q�6d   �� 5   
   $��1��6   $��1��6   $ 8��6I   $��Center=�6W   ��6/   �� *
Province+6W   �� 5   
   $��1��6   $��2��6!   $��省（市/区）��61   $��Center=�6?   $��Center=�6Z   $��10q�6w   �� 5   
   $��1��6   $��3��6   $ 8��6E   $��Center=�6S   ��6/   �� *City+6S   �� 5   
   $��1��6   $��4��6!   $��地/市��61   $��Center=�6<   $��Center=�6W   $��10q�6t   �� 5   
   $��1��6   $��5��6   $ 10��6G   $��Center=�6V   ��6/   �� *County+6V   �� 5   
   $��1��6   $��6��6!   $��县（市/区）��61   $��Center=�6?   $��Center=�6Z   $��10q�6w   �� 5    
   $��2��6   $��1��6   $��6��6/   $ 50��6b   $��Center=�6q   ��6C   �� *DetailAddress+6q   �� 5!   
   -   5        $��	btnCancel��5!   2   $��3��6   $��6��6   $��取消��6C   $	��100��6P   $��10q�6\   $��Right=�6h   $��Bottom=�6�   6�   �� 5"   
   -   5        $��btnOK��5"   2   $��3��6   $��6��6   $��保存��6?   $	��100��6L   $��10,10,120,10q�6X   $��Right=�6n   $��Bottom=�6�   6�   5#      5$      