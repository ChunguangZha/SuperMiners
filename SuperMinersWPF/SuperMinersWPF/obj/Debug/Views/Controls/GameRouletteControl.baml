   M S B A M L   `   `   `  ����  SuperMinersWPF7    1SuperMinersWPF.Views.Controls.GameRouletteControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          � 	Resources  5	         RouletteItemTextStyle �� 
TargetType �� Property �� Value  RouletteItemIcon��%�  &	        &	 �     �� 5
   
   " ��6.   .�5      ��  � ��$	 Top=�62   �� 5       � ��$ Center=�64   �� 5       � ��
 �  ��6+   �� 5       � ��$ 16(�6)   �� 5       � ��$
 Bold"�6+   5      �� 5   
   " ��6)   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ Center=�62   �� 5       � ��$ Center=�64   5      5      .��5      � .+�5   
   � -   5        $��panelRoulette��5      ���#d71837$y�Visible=�69    � ColumnDefinitions 5      �� 5      $	��220 �6#   �� 5      $��* �6#   �� 5      $	��490 �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5       �� 5!      $	��490 �6    �� 5"      .+�5$      � ���#9d0012$��0��6(   $��3��68    5%      �� 5&      $
��Auto �6$   �� 5'      $
��Auto �6$   �� 5(      $
��Auto �6$   �� 5)      �� 5*      $
��Auto �6$   .+�5,      � $��2q�6   ���#7d020c.+�5-      �� $��30q�6   $3��-/SuperMinersWPF;component/Resources/title.png��6(   5.      �� 5/      $��1��6    �� Orientation$ Vertical=�6*   .+�50      �� $@��:1、投注使用矿石，每投注一次消耗100矿石。��6    $��5q�6>   ���#ffdc15$
��Wrap=�6^   $��14(�6r   $
��Bold"�6�   �� 51      $C��=2、中奖为虚拟物品可直接在账户信息栏查看。��6    $��5q�6=   ���#ffdc15$
��Wrap=�6]   $��14(�6q   $
��Bold"�6   �� 52      $L��F3、实物奖品需填写联系方式，由在线客服人工发放。��6    $��5q�6@   ���#ffdc15$
��Wrap=�6`   $��14(�6t   $
��Bold"�6�   �� 53      $@��:4、【我的中奖纪录】提供查询您的中奖记录��6    $��5q�6<   ���#ffdc15$
��Wrap=�6\   $��14(�6p   $
��Bold"�6~   54      � 55      $��2��6   ���#7d020c.+�56      �� $��	中奖榜��6    ���#ffdc15$��18(�6@   $��5q�6N   $��Center=�6Y   $��Center=�6t   $
��Bold"�6�   57      �� 58      -   5        $��listAwardRecords��58   <   $��3��6   $��2��6(   $��5q�6T   $��Stretch=�6_   
������ 
�������59      � 5:      $��4��6   ���#7d020c.+�5;      �� -   5        $��btnViewMyWinAwardRecord��5;      $��查看我的中奖记录>��6<   ���#ffdc15$��14(�6e   $��5q�6s   $��Center=�6~   $��Stretch=�6�   $��0q�6�   $��Italic$�6�   ��6�   Z� 6�   5<      5=      � 5>      -   5        $��	panelCore��5>   0   $��1��6   $��2��6    $��30q�6A   ���#8e0710 5?      �� 5@      �� 5A      �� 5B      �� 5C       5E      �� 5F      �� 5G      �� 5H      �� 5I      .+�5K      � -   5        $��
awardItem1��5K   4   $��0��6   $��0��6$   $��5q�6F   ��6Q   �� *
Background+ 5L      �� 5M      $��6* �6(   �� 5N      $��4* �6(   .+�5P      �� #��[ 64   ��6   �� *Icon+64   �� 5Q      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5R      � 5S      -   5        $��
awardItem2��5S   4   $��0��6   $��1��6$   $��5q�6F   ��6Q   �� *
Background+ 5T      �� 5U      $��6* �6(   �� 5V      $��4* �6(   .+�5X      �� #��[ 64   ��6   �� *Icon+64   �� 5Y      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5Z      � 5[      -   5        $��
awardItem3��5[   4   $��0��6   $��2��6$   $��5q�6F   ��6Q   �� *
Background+ 5\      �� 5]      $��6* �6(   �� 5^      $��4* �6(   .+�5`      �� #��[ 64   ��6   �� *Icon+64   �� 5a      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5b      � 5c      -	   5        $��
awardItem4��5c   4   $��0��6   $��3��6$   $��5q�6F   ��6Q   �� *
Background+ 5d      �� 5e      $��6* �6(   �� 5f      $��4* �6(   .+�5h      �� #��[ 64   ��6   �� *Icon+64   �� 5i      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5j      � 5k      -
   5        $��
awardItem5��5k   4   $��1��6   $��3��6$   $��5q�6F   ��6Q   �� *
Background+ 5l      �� 5m      $��6* �6(   �� 5n      $��4* �6(   .+�5p      �� #��[ 64   ��6   �� *Icon+64   �� 5q      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5r      � 5s      -   5        $��
awardItem6��5s   4   $��2��6   $��3��6$   $��5q�6F   ��6Q   �� *
Background+ 5t      �� 5u      $��6* �6(   �� 5v      $��4* �6(   .+�5x      �� #��[ 64   ��6   �� *Icon+64   �� 5y      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5z      � 5{      -   5        $��
awardItem7��5{   4   $��3��6   $��3��6$   $��5q�6F   ��6Q   �� *
Background+ 5|      �� 5}      $��6* �6(   �� 5~      $��4* �6(   .+�5�      �� #��[ 64   ��6   �� *Icon+64   �� 5�      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5�      � 5�      -   5        $��
awardItem8��5�   4   $��3��6   $��2��6$   $��5q�6F   ��6Q   �� *
Background+ 5�      �� 5�      $��6* �6(   �� 5�      $��4* �6(   .+�5�      �� #��[ 64   ��6   �� *Icon+64   �� 5�      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5�      � 5�      -   5        $��
awardItem9��5�   4   $��3��6   $��1��6$   $��5q�6F   ��6Q   �� *
Background+ 5�      �� 5�      $��6* �6(   �� 5�      $��4* �6(   .+�5�      �� #��[ 64   ��6   �� *Icon+64   �� 5�      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5�      � 5�      -   5        $��awardItem10��5�   4   $��3��6   $��0��6$   $��5q�6G   ��6R   �� *
Background+ 5�      �� 5�      $��6* �6(   �� 5�      $��4* �6(   .+�5�      �� #��[ 64   ��6   �� *Icon+64   �� 5�      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5�      � 5�      -   5        $��awardItem11��5�   4   $��2��6   $��0��6$   $��5q�6G   ��6R   �� *
Background+ 5�      �� 5�      $��6* �6(   �� 5�      $��4* �6(   .+�5�      �� #��[ 64   ��6   �� *Icon+64   �� 5�      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5�      � 5�      -   5        $��awardItem12��5�   4   $��1��6   $��0��6$   $��5q�6G   ��6R   �� *
Background+ 5�      �� 5�      $��6* �6(   �� 5�      $��4* �6(   .+�5�      �� #��[ 64   ��6   �� *Icon+64   �� 5�      $��1��6    #��[  6H   ��6-   �� *	AwardName+6H   5�      �� 5�      -   5        $��btnStart��5�   [   $��1��6   $��1��6&   $��2��66   $��2��6G   $��10q�6k   ���	#FFFF2F0E
�������$��24(�6�   $��Italic$�6�   ��5�      � 5�       5�      �� 5�   "   �� 5�   "   .+�5�      �� $��10q�6$   $3��-/SuperMinersWPF;component/Resources/start.png��60   �� 5�      $��1��6$   $��20q�61   $7��1/SuperMinersWPF;component/Resources/starttext.png��6=   5�      5�      � 5�      -   5        $��panelWinedAwardItem��5�   Y   $��0��6   $��0��6$   $��4��64   $��4��6E   $y�	Collapsed=�6t   .+�5�      � 
������� J� Opacity$	 0.5Q�6.   � 5�      $��Center=�6   $��Center=�66   $	��100��6S   $	��100��6_   ���#ffdc15$ 1Q�6�    5�      �� 5�      $��6* �6,   �� 5�      $��4* �6,   .+�5�      �� -   5        $��imgWinedAwardItem��5�       �� 5�      -   5        $��txtWinedAwardItem��5�   1   $��1��6$   #��[  6J   5�      5�      5�      5�      � 5�   
   -   5        $��panelNotOpen��5�      $y�	Collapsed=�6#   .+�5�      �� $-��'该功能暂未开放，敬请期待...��6   $��20(�6/   $��Center=�6=   $��Center=�6X   5�      5�      5�      