   M S B A M L   `   `   `  ����G  CMain Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Question2	Question2  5          Question2.Page10   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:Question2     5        N� Title$  Page10��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��8* �6   .+�5   
   �� -   5        $��ContentControl��5   �   $��0��6!   $��10q�6.   $	��Top=�6:   $��Center=�6R   $��20(�6o   
���  ��6�   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��3* �6#   60   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��BookingDetails��5         CanUserAddRows$ False��6-      AutoGenerateColumns$ False��6D      
IsReadOnly$
 True��6`      SelectionMode$ Single=�6r    J� 	AllowDrop$ False��6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Room Number��6)   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 2* 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *Room.RoomNumber+6b     5      $	 
Start Date��6)   $
 3* 6[    6=   �� *	StartDate+6[     5      $	 End Date��6)   $
 3* 6W    6;   �� *	EndDate+6W     5      $	 Actual Price��6)   $
 3* 6_    6?   �� *ActualPrice+6_   5!      5"      5#      5$      