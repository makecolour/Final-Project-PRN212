   M S B A M L   `   `   `  ����G  CMain Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Question2	Question2  5          Question2.Page5   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:Question2     5        N� Title$  Page5��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $
��Auto �6   �� 5      $��8* �6   .+�5   
    � -   5        $��SubPageFrame��5      $��0��6   6   �� 5   
   $��1��6!   $��10q�6.   $	��Top=�6:   $��Center=�6R   $��20(�6o   
���  ��$ ��Manage Booking Reservation��6�   � 5   
   $��2��6    � ColumnDefinitions 5      �� 5      $��3* �6#   60   �� 5      $��* �6#   6/   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��Reservation��5         CanUserAddRows$ False��6+      AutoGenerateColumns$ False��6B      
IsReadOnly$
 True��6^      SelectionMode$ Single=�6p    J� 	AllowDrop$ False��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6)   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6^   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *BookingReservationId+6^     5      $	 Booking Date��6)   $
 2* 6_    6?   �� *BookingDate+6_     5       $	 Total Price��6)   $
 2* 6]    6>   �� *
TotalPrice+6]     5!      $	 Customer��6)   $
 3* 6i    6;   �� *Customer.CustomerFullName+6i   4  .System.Windows.Controls.DataGridCheckBoxColumn  5"      $	 Booking Status��6-   $

 1.5* 6E    6R   �� *BookingStatus+6v   5$      �� 5%      $��1��6   .+�5&      � $��1��6   .+�5'      �� $��5,0,5,0q�6    Reservation ID6A   �� 5(      -   5        $��txtId��5(   1   $��5,15,5,5q�6    �� 
IsReadOnly$
 True��6>   ��6P   ��  �� ElementName$ Reservation�� �� Path$' !SelectedItem.BookingReservationId� �� Mode$ OneWay=�5)      � 5*      $��2��6   .+�5+      �� $��5,0,5,0q�6    Booking Date6?   (  "System.Windows.Controls.DatePicker  5,      -   5        $��	dpBooking��5,   4   $��5,15,5,5q�6"   ${�False��6E      Text 6W   �� $ Reservation��$ SelectedItem.BookingDate�$ OneWay=�5-      � 5.      $��1��6   .+�5/      �� $��5,0,5,0q�6    Total Price6>   �� 50      -   5        $��txtPrice��50   1   $��5,15,5,5q�6   $
 True��6A   ��6S   �� $ Reservation��$ SelectedItem.TotalPrice�$ OneWay=�51      � 52      $��1��6   .+�53      �� $��5,0,5,0q�6    
Customer6;   �� 54      -   5        $��
cbCustomer��54   1   $��5,15,5,5q�6   6E   55      � 57      $��6��6   .+�58      �� $��5,0,5,0q�6    Booking Status6A   �� 59       �� Orientation$ 
Horizontal=�6!   $
��Left=�6:   $��5,20,0,5q�6U   .+�5:      	� -   5        $��active��5:   I   $��Active��6&   $��0,0,30,0q�67   6Y   	� 5;      -   5        $��deactive��5;   9   $��Deactive��6&   6K   5<      5=      � 5>       5?      �� 5@      $��* �6+   67   �� 5A      $��* �6+   67   .+�5C      �� -	   5        $��5q�5C      $��0��6(   $��30��68   .��6_   	Refresh6h   �� 5D      -
   5        $��5q�5D      $��1��6(   $��30��68   .��6b   
Add/Update6n   5E      � 5F       5G      �� 5H      $��* �6+   67   �� 5I      $��* �6+   67   .+�5K      �� -   5        $��5q�5K      $��0��6(   $��30��68   .��6Q   Edit6W   �� 5L      -   5        $��5q�5L      $��1��6(   $��30��68   .��6e   Delete6m   5M      5N      5O      5P      5Q      