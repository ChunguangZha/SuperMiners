   M S B A M L   `   `   `  ���� Y  USuperMinersCustomServiceSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:SuperMinersCustomServiceSystem.MyControl(SuperMinersCustomServiceSystem.MyControl  U    OSuperMinersCustomServiceSystem.View.Windows.EditPlayerRemoteServiceHandleWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     L	myControl6clr-namespace:SuperMinersCustomServiceSystem.MyControl       � Title$)  #EditPlayerRemoteServiceHandleWindow��$	��300��$	��400�� � WindowStyle$ 
ToolWindow=� � WindowStartupLocation$ CenterScreen=�.���  � ColumnDefinitions �� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto �.+��� $��玩家用户名：��$��10q�$��Center=�$��Right=��� -   $��txtPlayerUserName��$��0��$��1�� �� 
IsReadOnly . $��10q�$��Center=� �� 	MaxLength$	 150���� $��1��$��0��$��服务工程师：��$��10q�$��Center=�$��Right=��� -   $��txtWorkerName��$��1��$��1��$��10q�$��Center=�$	 150���� $��2��$��0��$��服务时间：��$��10q�$��Center=�$��Right=�F   @SuperMinersCustomServiceSystem.MyControl.MyDateTimePickerControl  -     Name$ myTimeServiceTime��$��2��$��1��$��10q�$��Center=��� $��3��$��0��$��服务内容：��$��10q�$	��Top=�$��Right=��� -   $��txtServiceContent��$��3��$��1��$��10q� �� TextWrapping$
 Wrap=�	 �� MaxLines$	 10000��
 �� AcceptsReturn
 .  �� 
AcceptsTab . �� -   $��	btnCancel��$��4��$��2��$��取消��$	��100��$��10q�$��Right=��� -   $��btnOK��$��4��$��2��$��确定��$	��100��$��10,10,130,10q�$��Right=�