   M S B A M L   `   `   `  ����  SuperMinersWPF1    +SuperMinersWPF.Views.ViewSingleNoticeWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  查看公告消息��5   	   $	��600��6   $	��600��6%    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.    � ShowInTaskbar .   � WindowStyle$ 
ToolWindow=�6Z   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5	       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��489* �6   �� 5      $
��Auto �6   .+�5   
   � $��2��6   $��2��6       NotifyPanelBackground#���   64   �� 5   
   -   5        $��txtNoticeTitle��5   (   $��2��6   $��24(�6U   $��Center=�6c   $��Center=�6~   $
��Bold"�6�   $��10q�6�   
���������6>   �� *Title+6�   �� 5   
   -   5        $��txtNoticeTime��5   1   $��1��6   $��1��6!   $��Italic$�6b   $��Center=�6u   $��5q�6�   
���������6F   �� *
TimeString+6�   �� 5   
   -   5        $��txtNoticeContent��5   3   $��2��6   $��2��6    �� 
IsReadOnly . $ �� HorizontalScrollBarVisibility$ Disabled=�6�   	 �� TextWrapping$
	 Wrap=�6�     LogoPanelBackground#���  6�   $��16(�6�   ��6K   �� *	Content+
 �� Mode$
 OneWay=�6�   �� 5   
   -   5        $��btnClose��5   2   $��3��6   $��2��6   $��关闭��6B   $	��100��6O   $��Right=�6[   $��5q�6w   6�   �� 5   
   -   5        $��
btnViewAll��5   2   $��3��6   $��2��6   $��查看所有通知��6D   $	��100��6U   $��Right=�6a   $��	5,5,150,5q�6}   5      5      