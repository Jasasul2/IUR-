   M S B A M L   `   `   `  ����W  SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TabItemStyle  �� 
TargetType �� Property �� Value �� 
StartPoint �� EndPoint  ButtonNormalGC1 � Color � Offset  ButtonNormalGC2  	TextColor �� Color �� 
TargetType	 J� SnapsToDevicePixels
 ��Name  ShadowBorder �� CornerRadius J� RenderTransformOrigin �� ScaleX �� ScaleY �� Triggers P� Property P� Value  ButtonHoveredBrush  DarkerTextColor N� EnterActions �� 
TargetName �� TargetProperty	 W� To g� Duration N� ExitActions  ButtonSelectedBrush �� 
TargetName3    -System.Windows.Media.Effects.DropShadowEffect    
BlurRadius    ShadowDepth    Color    Opacity W� From  TabControlStyle � RowDefinitions K� IsItemsHost�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %C  &	        7  7  7  7  7  7  7  7  &	 �	    �� 5      "  ��6!   .�5   
   ��  � �� 5      �� 5      $	 0,0&�6&   $	 1,1&�67   .��5	      � 8   $ 0Q�6L   � 5
      8  $ 1Q�6L   5      5      �� 5   
    � �� 5      �� 5      8  6"   5      �� 5   
    � ��$ 0q�6,   �� 5   
    � ��
 ���� 6(   �� 5   
    � ��$	 5,0q�6$   �� 5   
    � ��$ -5,0,0,0q�6#   �� 5   
    � ��$	 140��6"   �� 5   
    � ��$ 60��6#   �� 5   
    � ��$ 18(�6%   �� 5   
    � ��$
 Bold"�6'   �� 5   
    � ��$ Stretch=�6.   �� 5   
    � ��$ Center=�60   �� 5   
    � �� 5       �� 5!      " ��6"   .R�5"      � $
	 True��6   .+�5#      �� $
 Border��6!   8�� $ 5��5%   !   #��z��5&   !   #��z��5'   !   #��z��5(   !   #��z��5)   !   $ 0.5, 0.5&�5*   !   z�5+      �� 5,   "   $	 1.0Q�61   $	 1.0Q�6>   5.      �� 5/      $��Center=�6+   $��Center=�50   +   $
	 True��51   +   $
��True��52   +   #��z��53   +   $��Header��54   +   $|�False��55   +   57       58      P� 5;       �@J�IsMouseOver$
 True��69   . �5<      �� 8   � ��6;   �� 5=       � �� 5>   "   �� 5?   &   8  66   5A       5C      �� 5D   "   .��5E   &   �� .��5F   *   W� $ Border��6:   $9 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�5G   6    1.075H   6   $ 0:0:0.06D�6@   W� 5I   *   $ Border��6:   $9 3(UIElement.RenderTransform).(ScaleTransform.ScaleY)�5J   6    1.075K   6   $ 0:0:0.06D�6@   5L   '   5M   #    5O      �� 5P   "   .��5Q   &   �� .��5R   *   W� $ Border��6:   $9 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�5S   6    1.05T   6   $ 0:0:0.12D�6?   W� 5U   *   $ Border��6:   $9 3(UIElement.RenderTransform).(ScaleTransform.ScaleY)�5V   6    1.05W   6   $ 0:0:0.12D�6?   5X   '   5Y   #   5[      P� 5^       �@��
IsSelected$
 True��68   . �5_      �� 8   � ��6;   �� 5`       � �� 5a   "   �� 5b   &   8  66   5d      �� 5f      $ Border��6%    �@J�Effect 5g   "      5h   &   $ 10Q�67   $ 0Q�6G   $ Black��6W   $	 0.0Q�6e   5j       5l      �� 5m   "   .��5n   &   �� .��5o   *   W� $ Border��6:   $ Opacity�5p   >    1.05q   >    0.06I   $ 0:0:0.2D�6R   W� 5r   *   $ Border��6:   $ Opacity�5s   6    1.05t   6   $ 0:0:0.8D�6?   5u   '   5v   #   5x      5{      5}      5      �� 5�      "  ��6$   .�5�   
   ��  � ��
 ���� 6'   �� 5�   
    � ��$ 0q�6,   �� 5�   
    � ��
 ���� 6(   �� 5�   
    � ��$	 0,0q�6$   �� 5�   
    � ��$ 5,5,5,5q�6#   �� 5�   
    � ��$ Stretch=�6.   �� 5�   
    � ��$ Stretch=�60   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      � $
	 True��6    5�      �� 5�      $��80 �6,   �� 5�      $��* �6,   .+�5�      �� $��0��6#   
������ $
 True��5�   #   $��Stretch=�5�   #   $��Stretch=�5�   #   �� 5�      $��1��6!   $ 5��5�   !   #��z��5�   !   #��z��5�   !   #��z��5�   !   ._�5�      �� $��SelectedContent��6/   $
	 True��5�   /   #��z��5�   /   #��z��5�   /   5�      5�      5�      5�      5�      5�      