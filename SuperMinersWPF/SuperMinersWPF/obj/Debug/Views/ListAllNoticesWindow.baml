   M S B A M L   `   `   `  ����  SuperMinersWPF/    )SuperMinersWPF.Views.ListAllNoticesWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  查看系统消息��5   	   $	��600��6   $	��600��6%    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.    � WindowStyle$ 
ToolWindow=�6D   .��5      �  � RowDefinitions 5   
   �� 5      $	��53* �6   �� 5	      $
��519* �6   .+�5   
   � -   5        $��
panelTitle��5      ���	#FFAFBCBB.+�5      �� -   5        $��btnBack��5      $y�	Collapsed=�6$    � ToolTip$ 查看所有消息��6;   $��Center=�6L   $
��Left=�6g   $��5q�6�   $��0q�6�   ��5      �� 5      $4��./SuperMinersWPF;component/Resources/goback.png��6   $
��Left=�6T   5      �� 5      -   5        $��txtTitle��5      $��查看系统消息��6(   $��5q�66   $��18(�6A   $��Center=�6O   $��Center=�6j   5      � 5   
   -   5        $��panelViewSingleNotice��5      $��1��6   $��5q�69    � ColumnDefinitions 5      �� 5      �� 5       5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��489* �6    .+�5      � $��2��6   $��2��6$      NotifyPanelBackground#���   68   �� 5      -   5        $��txtNoticeTitle��5   ,   $��2��6   $��24(�6Y   $��Center=�6g   $��Center=�6�   $
��Bold"�6�   $��10q�6�   
���������6B   �� *Title+6�   �� 5      -   5        $��txtNoticeTime��5   5   $��1��6   $��1��6%   $��Italic$�6f   $��Center=�6y   $��5q�6�   
���������6J   �� *
TimeString+6�   �� 5       -   5        $��txtNoticeContent��5    7   $��2��6   $��2��6#    �� 
IsReadOnly . $ �� HorizontalScrollBarVisibility$ Disabled=�6�   	 �� TextWrapping$
	 Wrap=�6�     LogoPanelBackground#���  6�   $��16(�6�   "
 �� VerticalScrollBarVisibility$

 Auto=�6  ��6O   �� *	Content+ �� Mode$ OneWay=�6  5!      �� 5"   
   -   5        $��listboxAllNotices��5"      $��1��6   $��5q�68   $��Stretch=�6C   ��5#      �� 5$       �� 
TargetType" ��6    �� BasedOn# [B��6:   .�5%      -	   5        5&      ��5(      �� 5)      .R�5*      �  5+      �� 5,      $��50 �6/   �� 5-      �� 5.      $	��100��6/   .+�50      �� $ 图标��6    �� 51      $��1��6$   $��Center=�6K   $��5q�6f   ��64   �� *Title+6f   �� 52      $��2��6$   $��Center=�6J   $��Right=�6e   $��5q�6�   $��Italic$�6�   ��64   �� *Time+6�   53      54      56      57      58      