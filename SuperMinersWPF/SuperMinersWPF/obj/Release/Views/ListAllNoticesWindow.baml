   M S B A M L   `   `   `  ����   SuperMinersWPF/    )SuperMinersWPF.Views.ListAllNoticesWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  查看系统消息��$	��600��$	��600�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � WindowStyle$ 
ToolWindow=�.���  � RowDefinitions �� $	��53* ��� $
��519* �.+�� -   $��
panelTitle�����	#FFAFBCBB.+��� -   $��btnBack��$y�	Collapsed=� � ToolTip$ 查看所有消息��$��Center=�$
��Left=�$��5q�$��0q����� $4��./SuperMinersWPF;component/Resources/goback.png��$
��Left=��� -   $��txtTitle��$��查看系统消息��$��5q�$��18(�$��Center=�$��Center=�� -   $��panelViewSingleNotice��$��1��$��5q� � ColumnDefinitions �� ��  �� $
��Auto ��� $
��Auto ��� $
��489* �.+�� $��2��$��2��   NotifyPanelBackground#���   �� -   $��txtNoticeTitle��$��2��$��24(�$��Center=�$��Center=�$
��Bold"�$��10q�
����������� *Title+�� -   $��txtNoticeTime��$��1��$��1��$��Italic$�$��Center=�$��5q�
����������� *
TimeString+�� -   $��txtNoticeContent��$��2��$��2�� �� 
IsReadOnly . $ �� HorizontalScrollBarVisibility$ Disabled=�	 �� TextWrapping$
	 Wrap=�  LogoPanelBackground#���  $��16(�"
 �� VerticalScrollBarVisibility$

 Auto=����� *	Content+ �� Mode$ OneWay=��� -   $��listboxAllNotices��$��1��$��5q�$��Stretch=�����  �� 
TargetType" �� �� BasedOn# [B��.�-	   ���� .R��  �� $��50 ��� �� $	��100��.+��� $ 图标���� $��1��$��Center=�$��5q����� *Title+�� $��2��$��Center=�$��Right=�$��5q�$��Italic$����� *Time+