   M S B A M L   `   `   `  ����I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:SuperMinersWPF.MyControlSuperMinersWPF.MyControl  5   	   2    ,SuperMinersWPF.Views.InvitationFriendsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   <	myControl&clr-namespace:SuperMinersWPF.MyControl     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  邀请好友��5   	   $	��500��6   $	��500��6#    � 
ResizeMode$ NoResize=�6/    � WindowStartupLocation$ CenterScreen=�6E    � WindowStyle$ 
ToolWindow=�6j      MainPanelBackground#���   6�    � 	Resources 5        DataGridColumnHeaderStyle1 �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property " DataGridColumnHeaderBackground �� Value��%�   &	       7  �� 5   
   "  63   .�5	      ��  � ��8   6+   �� 5
       � ��
 �����6+   �� 5       � ��$ 14(�6)   �� 5       � ��$ 5q�6(   �� 5       � ��$ Center=�6;   5      5      .��5      � $��5q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      	 � RowDefinitions	 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��208* �6   �� 5      $	��63* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��我的推荐人��6   $��14(�6!   $��5q�6/   $��Right=�6:   $��Center=�6V   �� 5   
   $��1��6   $��14(�6S   
 �� 
IsReadOnly
 . $	��200��6s   $��5q�6�   ��6"   �� *UpReferrerUserName+ �� Mode$ OneWay=�6�   �� 5   
   $��1��6   $��0��6!   $��我邀请来的好友��61   $��14(�6@   $��5q�6N   $��Right=�6Y   $��Center=�6u   �� 5    
   $��1��6   $��1��6!   $��14(�61   $��5q�6?   �� 5!      $	�共��6    �� 5"      �6   �� *DownRefrerrerCount+$ OneWay=� �� 5#      $	�位��6   5$      R� 5%   
   $��2��6   $��2��6    $��Stretch=�6b   ��64   �� *ListDownRefrerrerTree+��5&      �� 5'       �� ItemsSource 6+   �� *ListDownRefrerrerTree+.R�5(      � $��Stretch=�6    5)      �� 5*      �� 5+      �� 5,      	 5.      �� 5/      �� 50      .+�52      �� $��Center=�6>   $��Center=�6[   $
��10,5q�6v   ��6$   �� *
UserName+6v   �� 53      $��1��6$   $��Center=�6P   $��Center=�6m   $
��10,5q�6�   ��64   �� *
RegisterIP+6�   �� 54      $��2��6$   $��Center=�6X   $��Center=�6u   $
��10,5q�6�   ��64   �� *RegisterTimeString+6�   55       �� ItemTemplate 56      �� 57      .R�58      � $��Stretch=�6#    59   "   �� 5:   &   �� 5;   &   �� 5<   &   	 5>   "   �� 5?   &   �� 5@   &   .+�5B   "   �� $��Center=�6F   $��Center=�6c   $
��10,5q�6~   ��6,   �� *
UserName+6~   �� 5C   "   $��1��6,   $��Center=�6X   $��Center=�6u   $
��10,5q�6�   ��6<   �� *
RegisterIP+6�   �� 5D   "   $��2��6,   $��Center=�6`   $��Center=�6}   $
��10,5q�6�   ��6<   �� *RegisterTimeString+6�   5E      5F      5H      5J      &   System.Windows.Controls.DataGrid  5L   
   -   5        $��datagridTopList��5L   4   $��2��6   $��2��6    $y�	Collapsed=�6K   $��12(�6�   
�������   AutoGenerateColumns .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserSortColumns .     GridLinesVisibility$ 
Horizontal=�6"     HorizontalGridLinesBrush# �  6C     AlternatingRowBackground �	#FFF3F3F3   HeadersVisibility$ Column=�6�     SelectionMode$ Single=�6�  ��6b   �� *ListDownRefrerrerTree+   Columns 5M      4  .System.Windows.Controls.DataGridTemplateColumn  5N      ,  &System.Windows.Controls.DataGridColumn   Header$ 	用户名��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 66      HeaderStyle# [ 6@      CellTemplate 5O      �� 5P      .R�5Q      � .+�5R   "   �� $��Center=�6F   $��Center=�6c   ��6,   �� *
UserName+6c   5S      5T      5V        5W      $ 注册IP��6)   $ * 67   # [ 6A    5X      �� 5Y      .R�5Z      � .+�5[   "   �� $��Center=�6H   $��Center=�6e   $��5q�6�   ��6,   �� *
RegisterIP+6�   5\      5]      5_        5`      $ 注册时间��6)   $ * 67   # [ 6A    5a      �� 5b      .R�5c      � .+�5d   "   �� $��Center=�6P   $��Center=�6m   $��5q�6�   ��6,   �� *RegisterTimeString+6�   5e      5f      5h      5j      �� 5k   
   $��3��6   $$��请将邀请码复制给好友��6!   $��5q�63   $��Right=�6>   �� 5l   
   -   5        $��txtInvitationCode��5l   /   $��3��6   $��1��6   
 . $��5q�6Z    �� TextWrapping$
 Wrap=�6e   �� 5m   
   -   5        $��txtReferrerMsg��5m   1   $��4��6   $��1��6!   $��5q�6G   $
��Wrap=�6R   �� 5n   
   -   5        $��btnOK��5n   2   $��5��6   $��2��6   $��复制��6?   $��75��6L   $��	5,5,100,5q�6W   $��Right=�6j   6�   �� 5o   
   -   5        $��	btnCancel��5o   2   $��5��6   $��2��6   $��关闭��6C   $��75��6P   $��5q�6[   $��Right=�6f   6�   5p      5q      