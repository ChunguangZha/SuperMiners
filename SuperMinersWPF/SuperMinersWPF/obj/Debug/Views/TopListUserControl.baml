   M S B A M L   `   `   `  ����  SuperMinersWPF-    'SuperMinersWPF.Views.TopListUserControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          � 	Resources  5	         TopListInfoTemplate  RadioButtonStyle1 �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius J� SnapsToDevicePixels ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName  DataGridColumnHeaderStyle1=  7System.Windows.Controls.Primitives.DataGridColumnHeader " DataGridColumnHeaderBackground��%�  &	        &	     &	 �    7  �� 5
   
   .R�5      � $��Stretch=�6   .+�5      �� 
�������$��1q�6,   ._�5      �� $��Center=�67   $��Center=�6T   $��5q�6o   ��6    �� *Index+6o   5      �� 5      $��1��6   
�������$��1q�6<   ._�5      �� $��Center=�6:   $��Center=�6W   $��5q�6r   ��6    �� *
NickName+6r   5      �� 5      $��2��6   
�������$��1q�6<   ._�5      �� $��Center=�67   $��Center=�6T   $��5q�6o   ��6    �� *Value+6o   5      5      5      �� 5   
   " 	�6*   .�5      ��  � ��$ 1q�60   �� 5       � �� �	#FF4CADA66,   �� 5       � �� �	#FF4CADA66+   �� 5       � ��$
 10,5q�6'   �� 5       � ��$ 14(�6)   �� 5       � ��$ Center=�6;   �� 5       � �� 5      �� 5       " 	�6&   .R�5!      �� $ templateRoot��6!   $ 2��67   #��z��6H   #��z��6|    . ._�5"      �� $ contentPresenter��6/   $��1��6I   |�.  #��z��6k   #��z��6�   ��. # z 6�   #��z��6(  5#      	 5$      P� 5%      
 � �� . . �5&   "   ��  � ��$ 4,-1,0,0q�6<   5'      P� 5(      
 �@J�IsMouseOver . . �5)   "   �� $ templateRoot��6?    � �� �	#FFC8ECEA6Y   5*      P� 5+      
 � {� .  . �5,   "   �� $ templateRoot��6?    � �� �	#FFF0F0F06Y   5-      P� 5.      
 � �� . . �5/   "   �� $ templateRoot��6?    � �� �	#FFC8ECE76Y   50      P� 51      
 �@d�	IsChecked$
 trueY�6;   . �52   "   �� $ templateRoot��6?    � �� �	#FFC8ECE76Y   53      P� 54       6;   Z� 
 �@d�	IsChecked. �55   "   �� $ templateRoot��6?    � �� �	#FFC8ECE76Y   56      58      5:      5;      �� 5<   
   "  63   .�5=      ��  � ��8   6+   �� 5>       � ��
 �����6+   �� 5?       � ��$ 14(�6)   �� 5@       � ��$ 5q�6(   �� 5A       � ��$ Center=�6;   5B      5C      .��5D      � ���	#FFF0F0F0 � ColumnDefinitions 5E   
   �� 5F      $
��Auto �6   �� 5G      .+�5I   
   �� $��10q�6   .+�5J      	� -   5        $��rbtnExpTopList��5J      $��	贡献榜��60   #��[ 6>   $
��20,5q�6i    	� 	GroupName$ TopList��6x    d� 	IsChecked$
 TrueY�6�   6�   	� 5K      -   5        $��rbtnBuyTopList��5K      $��	交易榜��60   #��[ 6>   $
��20,5q�6i   $ TopList��6x   6�   	� 5L      -   5        $��rbtnRefrerTopList��5L      $��	推广榜��63   #��[ 6A   $
��20,5q�6l   $ TopList��6{   6�   	� 5M      -   5        $��rbtnMinersTopList��5M      $��	矿工榜��63   #��[ 6A   $
��20,5q�6l   $ TopList��6{   6�   5N      � 5O   
   -   5        $��panelTopListPage��5O      $��1��6   
�������.+�5P      &   System.Windows.Controls.DataGrid  -   5        $��datagridTopList��5P   $   $��1��6   $��12(�6;   
�������   AutoGenerateColumns .     CanUserReorderColumns .     CanUserResizeColumns .     CanUserSortColumns .     GridLinesVisibility$ 
Horizontal=�6�      HorizontalGridLinesBrush# �  6�      AlternatingRowBackground �	#FFF3F3F3   HeadersVisibility$ Column=�6_     SelectionMode$ Single=�6z     Columns 5Q      4  .System.Windows.Controls.DataGridTemplateColumn  5R      ,  &System.Windows.Controls.DataGridColumn   Header$ 排名��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 69      HeaderStyle# [ 6C      CellTemplate 5S      �� 5T      .R�5U   "   � .+�5V   &   �� $
��None=�6J   $��Center=�6Y   $��Center=�6v   ��6,   �� *
IndexImage+6v   �� 5W   &   $��Center=�6i   $��Center=�6�   ��60   �� *Index+��6G   �� *
Foreground+6�   5X   #   5Y      5[        5\      $ 玩家��6-   $ * 69   # [ 6C    5]      �� 5^      .R�5_   "   � .+�5`   &   �� $��Center=�6J   $��Center=�6g   $��5q�6�   ��60   �� *
UserName+6�   5a   #   5b      5d        5e      $ 	贡献值��6-   $ * 6:   # [ 6D    5f      �� 5g      .R�5h   "   � .+�5i   &   �� $��Center=�6G   $��Center=�6d   $��5q�6   ��60   �� *Value+6   5j   #   5k      5m      5o      5p      5q      5r      