   M S B A M L   `   `   `  ����I  ESuperMinersWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SuperMinersWPF.ViewsSuperMinersWPF.Views  5   	   )    #SuperMinersWPF.Views.WinRechargeRMB   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6myViews"clr-namespace:SuperMinersWPF.Views     5   	      SubWindowStyle#��[  5   	     � Title$  	RMB充值��5   	    � WindowStartupLocation$ CenterOwner=�5   	   .��5      �  � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��1元人民币=1RMB。��6   $��18(�6'   $��5q�65   �� 5   
   $��1��6   $��5q�6"   .+�5      �� $K��E请用手机支付宝扫描下面二维码，并输入充值金额。��6   �� 5       �� Orientation$ 
Horizontal=�6   .+�5      �� $6��0如果没有手机支付宝客户端，请链接��6   �� 5      $	��5,0q�6   �� 5      $��www.alipay.com?�6    .�6=   	支付宝6B   5      5      �� 5      $ 
Horizontal=�6   .+�5      �� $E��?在打开的支付宝转账页面中，填写收款账户为：��6   �� 5      $	��XXX��6   
���  �� �� 
IsReadOnly . $��0q�6H   $	��5,0q�6\   5      5      �� 5   
   $��2��6   $��5q�6   �� 5    
   $��3��6   $��5q�6"   $ 
Horizontal=�6-   .+�5!      �� $!��付款成功后，请点击��6   $��Center=�6)   �� 5"      -   5        $��btnOK��5"      $��确认付款��6"   $
��10,5q�61   $	��5,0q�6@   �� 5#      $N��H并等待服务器验证，验证成功后会立即向您的账户充值��6   $��Center=�68   5$      5%      5&      