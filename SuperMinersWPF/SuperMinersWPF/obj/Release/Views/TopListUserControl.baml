   M S B A M L   `   `   `  ����   SuperMinersWPF-    'SuperMinersWPF.Views.TopListUserControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     -     � 	Resources     TopListInfoTemplate  RadioButtonStyle1 �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius J� SnapsToDevicePixels ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName  DataGridColumnHeaderStyle1=  7System.Windows.Controls.Primitives.DataGridColumnHeader " DataGridColumnHeaderBackground��%�  &	        &	 0    &	 ?    7  �� .R�� $��Stretch=�.+��� 
�������$��1q�._��� $��Center=�$��Center=�$��5q����� *Index+�� $��1��
�������$��1q�._��� $��Center=�$��Center=�$��5q����� *
NickName+�� $��2��
�������$��1q�._��� $��Center=�$��Center=�$��5q����� *Value+�� " 	�.���  � ��$ 1q���  � �� �	#FF4CADA6��  � �� �	#FF4CADA6��  � ��$
 10,5q���  � ��$ 14(���  � ��$ Center=���  � �� �� " 	�.R��� $ templateRoot��$ 2��#��z��#��z�� . ._��� $ contentPresenter��$��1��|�.  #��z��#��z����. # z #��z��	 P� 
 � �� . . ���  � ��$ 4,-1,0,0q�P� 
 �@J�IsMouseOver . . ��� $ templateRoot�� � �� �	#FFC8ECEAP� 
 � {� .  . ��� $ templateRoot�� � �� �	#FFF0F0F0P� 
 � �� . . ��� $ templateRoot�� � �� �	#FFC8ECE7P� 
 �@d�	IsChecked$
 trueY�. ��� $ templateRoot�� � �� �	#FFC8ECE7P�  Z� 
 �@d�	IsChecked. ��� $ templateRoot�� � �� �	#FFC8ECE7�� "  .���  � ��8   ��  � ��
 �������  � ��$ 14(���  � ��$ 5q���  � ��$ Center=�.��� ���	#FFF0F0F0 � ColumnDefinitions �� $
��Auto ��� .+��� $��10q�.+�	� -   $��rbtnExpTopList��$��	贡献榜��#��[ $
��20,5q� 	� 	GroupName$ TopList�� d� 	IsChecked$
 TrueY�	� -   $��rbtnBuyTopList��$��	交易榜��#��[ $
��20,5q�$ TopList��	� -   $��rbtnRefrerTopList��$��	推广榜��#��[ $
��20,5q�$ TopList��	� -   $��rbtnMinersTopList��$��	矿工榜��#��[ $
��20,5q�$ TopList��� -   $��panelTopListPage��$��1��
�������.+�&   System.Windows.Controls.DataGrid  -   $��datagridTopList��$��1��$��12(�
�������   AutoGenerateColumns .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserSortColumns .     GridLinesVisibility$ 
Horizontal=�   HorizontalGridLinesBrush# �     AlternatingRowBackground �	#FFF3F3F3   HeadersVisibility$ Column=�   SelectionMode$ Single=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 排名��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    HeaderStyle# [    CellTemplate �� .R�� .+��� $
��None=�$��Center=�$��Center=����� *
IndexImage+�� $��Center=�$��Center=����� *Index+���� *
Foreground+  $ 玩家��$ * # [  �� .R�� .+��� $��Center=�$��Center=�$��5q����� *
UserName+  $ 	贡献值��$ * # [  �� .R�� .+��� $��Center=�$��Center=�$��5q����� *Value+