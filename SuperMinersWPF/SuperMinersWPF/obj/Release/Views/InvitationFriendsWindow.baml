   M S B A M L   `   `   `  ���� I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  2    ,SuperMinersWPF.Views.InvitationFriendsWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     <	myControl&clr-namespace:SuperMinersWPF.MyControl     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  邀请好友��$	��500��$	��500�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$ 
ToolWindow=�   MainPanelBackground#���    � 	Resources   DataGridColumnHeaderStyle1 �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property " DataGridColumnHeaderBackground �� Value��%�   &	       7  �� "  .���  � ��8   ��  � ��
 �������  � ��$ 14(���  � ��$ 5q���  � ��$ Center=�.��� $��5q� � ColumnDefinitions �� $
��Auto ��� 	 � RowDefinitions	 �� $
��Auto ��� $
��Auto ��� $
��208* ��� $	��63* ��� $
��Auto ��� $
��Auto �.+��� $��我的推荐人��$��14(�$��5q�$��Right=�$��Center=��� $��1��$��14(�
 �� 
IsReadOnly
 . $	��200��$��5q����� *UpReferrerUserName+ �� Mode$ OneWay=��� $��1��$��0��$��我邀请来的好友��$��14(�$��5q�$��Right=�$��Center=��� $��1��$��1��$��14(�$��5q��� $	�共�� �� ��� *DownRefrerrerCount+$ OneWay=� �� $	�位��R� $��2��$��2��$��Stretch=����� *ListDownRefrerrerTree+����  �� ItemsSource �� *ListDownRefrerrerTree+.R�� $��Stretch=� �� �� �� 	 �� �� .+��� $��Center=�$��Center=�$
��10,5q����� *
UserName+�� $��1��$��Center=�$��Center=�$
��10,5q����� *
RegisterIP+�� $��2��$��Center=�$��Center=�$
��10,5q����� *RegisterTimeString+ �� ItemTemplate �� .R�� $��Stretch=� �� �� �� 	 �� �� .+��� $��Center=�$��Center=�$
��10,5q����� *
UserName+�� $��1��$��Center=�$��Center=�$
��10,5q����� *
RegisterIP+�� $��2��$��Center=�$��Center=�$
��10,5q����� *RegisterTimeString+&   System.Windows.Controls.DataGrid  -   $��datagridTopList��$��2��$��2��$y�	Collapsed=�$��12(�
�������   AutoGenerateColumns .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserSortColumns .     GridLinesVisibility$ 
Horizontal=�   HorizontalGridLinesBrush# �     AlternatingRowBackground �	#FFF3F3F3   HeadersVisibility$ Column=�   SelectionMode$ Single=����� *ListDownRefrerrerTree+   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	用户名��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    HeaderStyle# [    CellTemplate �� .R�� .+��� $��Center=�$��Center=����� *
UserName+  $ 注册IP��$ * # [  �� .R�� .+��� $��Center=�$��Center=�$��5q����� *
RegisterIP+  $ 注册时间��$ * # [  �� .R�� .+��� $��Center=�$��Center=�$��5q����� *RegisterTimeString+�� $��3��$$��请将邀请码复制给好友��$��5q�$��Right=��� -   $��txtInvitationCode��$��3��$��1��
 . $��5q� �� TextWrapping$
 Wrap=��� -   $��txtReferrerMsg��$��4��$��1��$��5q�$
��Wrap=��� -   $��btnOK��$��5��$��2��$��复制��$��75��$��	5,5,100,5q�$��Right=��� -   $��	btnCancel��$��5��$��2��$��关闭��$��75��$��5q�$��Right=�