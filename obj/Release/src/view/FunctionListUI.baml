   M S B A M L   `   `   `  ���� 
  IGUWPF,    &IGUWPF.src.view.Windows.FunctionLitsUI   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � Icon$-  '/IGUWPF;component/Images/Icons/plot.png�� � Title$ WinLab��$	��450��$	��600��$	��450��$	��600�� � 	Resources    ToolBarButtonStyle �� Property �� Value  ToolBarIconStyle  FunctionValuesTextBoxStyle  FunctionValuesLabelStyle  ConfirmationButtonsStyle��%3  &	        &	 J     &	 ~     &	 �     &	 �     �� .���  � ��$ 3q���  � ��$ 0q���  � ��
 ���� �� .���  � ��$ 20����  � ��$ 20���� .���  � ��$ 5q���  � ��$	 100����  � ��$ 0���� .���  � ��$ 5q��� .���  � ��$ 5q�.��]� .+�o� $��Center=�
�������$	��Top=�.@�m� -   $��SettingsButton�� � ToolTip$* $Cambiar parametros de representacion��#��[  ���� .+��� $1��+/IGUWPF;component/Images/Icons/settings.png��#��[ m� -   $��SaveFileButton��$ Guardar proyecto��#��[  ���� .+��� $-��'/IGUWPF;component/Images/Icons/save.png��#��[ m� -   $��OpenFileButton��$ Abrir proyecto��#��[  ���� .+��� $-��'/IGUWPF;component/Images/Icons/open.png��#��[ �� -   $��MainTabControl��.@��� -   $��FunctionListTab��$��Lista de funciones��.���� 
������$��1q�$
��Auto=�.��&   System.Windows.Controls.DataGrid  -   $��FunctionDataGrid��   AutoGenerateColumns .     SelectionMode$ Single=�   CanUserDeleteRows .  	   CanUserAddRows	 .  
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Nombre��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 3* 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Mode$ TwoWay=� �� Path$
 Name� �� UpdateSourceTrigger$ PropertyChanged=�  $ 	Expresion��   
IsReadOnly . $ 3*  �� $ TwoWay=�$ CalculatorStr�$ 	LostFocus=�4  .System.Windows.Controls.DataGridTemplateColumn  $ Color��$ 2*    CellTemplate ��  �� DataType$ Function��.R�#  Xceed.Wpf.Toolkit.ColorPicker     SelectedColor �� $ TwoWay=�$ Color�$ PropertyChanged=�  $ Visibilidad��$ 2*  �� .R���  d� 	IsChecked �� $ TwoWay=�$ 
IsVissible�$ PropertyChanged=� � ContextMenu �� -   $��DataGridContextMenu��.@�m� -   $%��DataGridContextMenu_AddFunction��$��Añadir funcion��m� -	   $0��*DataGridContextMenu_DeleteSelectedFunction��$��Eliminar��m� -
   $8��2DataGridContextMenu_EditSelectedFunctionExpression��$��Editar expresion���� -   $��Anadir��$��Definicion de grafica��.���  � RowDefinitions �� $��3* ��� $��2* �.+��� $��Definicion de grafica��$��0��.���  �� �� �� �� �� .+�� $��0��.+��� $��5q�$��Nombre:���� -   $��FunctionNameTextBox��$��5q�$	��200��� $��1��$��0��.+��� $��
Color:    ��$��5q�  -   $��ColorSelector��$��5q�$	��200��$ BlackX�� $��2��.+��� $��Funcion:��$��5q��� -   $��FunctionComboBox��$��5q�$	��200��� $��3��.+��� $��a:��#��[ �� -   $��AValueTextBox��#��[ �� $��b:��#��[ �� -   $��BValueTextBox��#��[ �� -   $��CLabel��$��c:��$y�Hidden=�#��[ �� -   $��CValueTextBox��$y�Hidden=�#��[ � $��4�� � ColumnDefinitions �� $��* ��� $��* �.+��� -   $��PreviewButton��$��Generar vista previa��$��Right=�$��0��#��[ �� -   $��AddFunctionButton��$��Anadir funcion��$��1��#��[ �� $��Vista previa��$��1��.���� -   $��PreviewPanel��}�. 
�������