   M S B A M L   `   `   `  ����H  DClientProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ClientProjectClientProject  5   	       ClientProject.Home   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ClientProject     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$
  home��5	   	    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�6   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� -   5        $��templ��5      6   �� 5   
   $!��/images/background_home.jpg��6   $��UniformToFill=�65   $��5��6M   $��5��6a   �� 5   
   $&�� images/background_label_home.jpg��6   $��UniformToFill=�6:   $��0��6R   $��5��6_   �� 5    
   -   5        $��title��5       $��Segoe UI Black)�6   $��24(�69   $��Center=�6G   $��Center=�6d   $��0��6   $��0��6�   $��4��6�   �� 5!   
   -   5        $��title2��5!      $��Segoe UI Black)�6   $��24(�6:   $��Center=�6H   $��Center=�6e   $��0��6�   $��4��6�   $��4��6�   �� 5#   
   -   5        $��nameuser��5#      $��Segoe UI Black)�6    $��24(�6<   $��Center=�6J   $	��Top=�6g   $��0��6   $��2��6�   �� 5$   
   -   5        $��
namefamily��5$      $��Segoe UI Black)�6"   $��24(�6>   $��Center=�6L   $��Bottom=�6i   $��0��6�   $��2��6�   �� 5&   
   -   5        $��	newAnimal��5&      $��Segoe UI Black)�6"   $��14(�6>   $��3��6L   $��4��6\   $��Center=�6i   $��Bottom=�6�   .��6�   add a new animal6�   �� 5'   
   -   5        $��editUser��5'      $��Segoe UI Black)�6!   $��14(�6=   $��1��6K   $��0��6[   $��Center=�6h   $	��Top=�6�   .��6�   edit user information6�   �� 5(   
   -   5        $��
editfamily��5(      $��Segoe UI Black)�6#   $��13(�6?   $��1��6M   $��4��6]   $��Center=�6j   $��Bottom=�6�   .��6�   edit family information6�   �� 5)   
   $��0��6   $��1��6    $��Center=�6-   $��Center=�6J   $$��upload your image to see it :)��6e   �� 5+   
    �� CornerRadius$ 80��6   
�������$��0��66   $��0��6C   $��2��6S   ._�5,      �� -	   5        $��	userimage��5,      6'   5-      �� 5.   
   $��0��6   $��3��6    $��Center=�6-   $��Center=�6J   $$��upload your image to see it :)��6e   �� 5/   
   $ 80��6   
�������$��3��66   $��0��6C   $��2��6S   ._�50      �� -
   5        $��familyimage��50      6)   51      �� 53   
   -   5         ��Name$ animal1��53      {�.  $��1��64   $��1��6D   $
��Auto��6j   $
��Auto��6w   $��Stretch=�6�   $��Stretch=�6�   .��54      �� -   5         ��Name$ image1��54      $��UniformToFill=�6$   55      �� 56   
   -   5        $ animal2��56      {�.  $��2��64   $��1��6D   $
��Auto��6j   $
��Auto��6w   $��Stretch=�6�   $��Stretch=�6�   .��57      �� -   5        $ image2��57      $��UniformToFill=�6$   58      �� 59   
   -   5        $ animal3��59      {�.  $��3��64   $��1��6D   $
��Auto��6j   $
��Auto��6w   $��Stretch=�6�   $��Stretch=�6�   .��5:      �� -   5        $ image3��5:      $��UniformToFill=�6$   5;      �� 5<   
   -   5        $ animal4��5<      {�.  $��1��64   $��2��6D   $
��Auto��6j   $
��Auto��6w   $��Stretch=�6�   $��Stretch=�6�   .��5=      �� -   5        $ image4��5=      $��UniformToFill=�6$   5>      �� 5?   
   -   5        $ animal5��5?      {�.  $��2��64   $��2��6D   $
��Auto��6j   $
��Auto��6w   $��Stretch=�6�   $��Stretch=�6�   .��5@      �� -   5        $ image5��5@      $��UniformToFill=�6$   5A      �� 5B   
   -   5        $ animal6��5B      {�.  $��3��64   $��2��6D   $
��Auto��6j   $
��Auto��6w   $��Stretch=�6�   $��Stretch=�6�   .��5C      �� -   5        $ image6��5C      $��UniformToFill=�6$   5D      �� 5E   
   -   5        $ animal7��5E      {�.  $��1��64   $��3��6D   $
��Auto��6j   $
��Auto��6w   $��Stretch=�6�   $��Stretch=�6�   .��5F      �� -   5        $ image7��5F      $��UniformToFill=�6$   5G      �� 5H   
   -   5        $ animal8��5H      {�.  $��2��64   $��3��6D   $
��Auto��6j   $
��Auto��6w   $��Stretch=�6�   $��Stretch=�6�   .��5I      �� -   5        $ image8��5I      $��UniformToFill=�6$   5J      �� 5K   
   -   5        $ animal9��5K      {�.  $��3��64   $��3��6D   $
��Auto��6j   $
��Auto��6w   $��Stretch=�6�   $��Stretch=�6�   .��5L      �� -   5        $ image9��5L      $��UniformToFill=�6$   5M      �� 5O   
   -   5        $��nameanimal1��5O      $��1��6#   $��1��63   $	��Top=�6@   $��Center=�6X   $�� ��6u   $��Segoe UI Black)�6�   $��25(�6�   6�   �� 5P   
   -   5        $��nameanimal2��5P      $��2��6#   $��1��63   $	��Top=�6@   $��Center=�6X   $�� ��6u   $��Segoe UI Black)�6�   $��25(�6�   6�   �� 5Q   
   -   5        $��nameanimal3��5Q      $��3��6#   $��1��63   $	��Top=�6@   $��Center=�6X   $�� ��6u   $��Segoe UI Black)�6�   $��25(�6�   6�   �� 5R   
   -    5        $��nameanimal4��5R      $��1��6#   $��2��63   $	��Top=�6@   $��Center=�6X   $�� ��6u   $��Segoe UI Black)�6�   $��25(�6�   6�   �� 5S   
   -!   5        $��nameanimal5��5S      $��2��6#   $��2��63   $	��Top=�6@   $��Center=�6X   $�� ��6u   $��Segoe UI Black)�6�   $��25(�6�   6�   �� 5T   
   -"   5        $��nameanimal6��5T      $��3��6#   $��2��63   $	��Top=�6@   $��Center=�6X   $�� ��6u   $��Segoe UI Black)�6�   $��25(�6�   6�   �� 5U   
   -#   5        $��nameanimal7��5U      $��1��6#   $��3��63   $	��Top=�6@   $��Center=�6X   $�� ��6u   $��Segoe UI Black)�6�   $��25(�6�   6�   �� 5V   
   -$   5        $��nameanimal8��5V      $��2��6#   $��3��63   $	��Top=�6@   $��Center=�6X   $�� ��6u   $��Segoe UI Black)�6�   $��25(�6�   6�   �� 5W   
   -%   5        $��nameanimal9��5W      $��3��6#   $��3��63   $	��Top=�6@   $��Center=�6X   $�� ��6u   $��Segoe UI Black)�6�   $��25(�6�   6�   �� 5[   
   -&   5        $
��Left=�5[      $	��Top=�68      MaterialDesignFlatButton#��[  6P   .��5\      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6&   5]      5a      5b      