   M S B A M L   `   `   `  ����   SuperMinersWPF1    +SuperMinersWPF.Views.ViewSingleNoticeWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  查看公告消息��$	��600��$	��600�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � WindowStyle$ 
ToolWindow=�.���  � ColumnDefinitions �� ��  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��489* ��� $
��Auto �.+�� $��2��$��2��   NotifyPanelBackground#���   �� -   $��txtNoticeTitle��$��2��$��24(�$��Center=�$��Center=�$
��Bold"�$��10q�
����������� *Title+�� -   $��txtNoticeTime��$��1��$��1��$��Italic$�$��Center=�$��5q�
����������� *
TimeString+�� -   $��txtNoticeContent��$��2��$��2�� �� 
IsReadOnly . $ �� HorizontalScrollBarVisibility$ Disabled=�	 �� TextWrapping$
	 Wrap=�  LogoPanelBackground#���  $��16(����� *	Content+
 �� Mode$
 OneWay=��� -   $��btnClose��$��3��$��2��$��关闭��$	��100��$��Right=�$��5q��� -   $��
btnViewAll��$��3��$��2��$��查看所有通知��$	��100��$��Right=�$��	5,5,150,5q�