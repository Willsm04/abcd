   M S B A M L   `   `   `  ????G  CBeauty_Motos, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Beauty_MotosBeauty_Motos  5   	       Beauty_Motos.ICliente   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Beauty_Motos     5   	   -   5          ? Title$  Cliente??5       ? 
ResizeMode$ NoResize=?6   $	??646??6+   $
??1481??68    ? WindowStyle$
 None=?6E    ? WindowStartupLocation$ CenterScreen=?6X    ? Icon$ Imagens/add-user.png??6}   ??5
      ?? 5   
    ?? EndPoint$ 0.5,1&?6    ?? 
StartPoint$ 0.5,0&?6/   .??5      ?  ? Color$ 	#FFF7A56B??6   ? 5      $ 	#FFF76E6E??6    ? Offset$ 1Q?6-   5      .??5      ? $	??646??6   $??Center=?6   	 ? RowDefinitions	 5   
   ?? 5      .+?5   
   ?? -   5        
 ??Name$
 label??5      $??Cadastro do Clientes??6   $
??Left=?6>   $??
388,12,0,0q?6Y   $	??Top=?6m   $??56??6?   $	??710??6?   $??40(?6?   $??Arial Black)?6?   $??Center=?6?   ?? 5   
   $??Nome Completo: ??6   $
??Left=?6*   $??
48,175,0,0q?6E   $	??Top=?6Y   $??37??6q   $	??203??6}   $??25(?6?   $??Right=?6?   $??Franklin Gothic Medium)?6?   ?? 5   
   $??CPF: ??6   $
??Left=?6    $??108,291,0,0q?6;   $	??Top=?6P   $??37??6h   $??61??6t   $??25(?6   $??Right=?6?   $??Franklin Gothic Medium)?6?   ?? 5   
   $??Logradouro: ??6   $
??Left=?6'   $??
75,347,0,0q?6B   $	??Top=?6V   $??37??6n   $	??141??6z   $??25(?6?   $??Right=?6?   $??Franklin Gothic Medium)?6?   ?? 5   
   $??Bairro: ??6   $
??Left=?6#   $??
96,457,0,0q?6>   $	??Top=?6R   $??37??6j   $??82??6v   $??25(?6?   $??Right=?6?   $??Franklin Gothic Medium)?6?   ?? 5   
   $??Telefone Celular: ??6   $
??Left=?6-   $??
44,235,0,0q?6H   $	??Top=?6\   $??37??6t   $	??203??6?   $??25(?6?   $??Right=?6?   $??Franklin Gothic Medium)?6?   ?? 5   
   $??Cidade: ??6   $
??Left=?6#   $??
91,519,0,0q?6>   $	??Top=?6R   $??37??6j   $??92??6v   $??25(?6?   $??Right=?6?   $??Franklin Gothic Medium)?6?   ?? 5   
   $??CEP: ??6   $
??Left=?6    $??106,404,0,0q?6;   $	??Top=?6P   $??38??6h   $??58??6t   $??25(?6   $??Right=?6?   $??Franklin Gothic Medium)?6?   ?? 5   
   -   5         ??Name$ txtNomeCompleto??5       ?? 	MaxLength$ 30??6+   $
??Left=?6`   $??260,183,0,0q?6{    ?? TextWrapping$
 Wrap=?6?   $	??Top=?6?   $	??501??6?   $??23(?6?    ?? MaxLines$ 1??6?   $??1??6?   ???	#FF9898986<  ?? 5   
   -   5        $ txtTelefoneCelular??5      $ 13??6?   $
??Left=?6?   $??260,240,0,0q?6?   $
 Wrap=?6?   $	??Top=?6?   $	??292??6?   $??23(?6?   $ 1??6	  $??2??6  ???	#FF9898986;  ?? 5   
   -   5        $ txtCPF??5      $ 14??6C   $
??Left=?6R   $??260,293,0,0q?6m   $
 Wrap=?6?   $	??Top=?6?   $	??292??6?   $??23(?6?   $ 1??6?   $??3??6?   ???	#FF9898986?   ?? 5    
   -   5        $ txtLogradouro??5       $ 42??6)   $
??Left=?68   $??260,347,0,0q?6S   $
 Wrap=?6h   $	??Top=?6|   $	??501??6?   $??23(?6?   $ 1??6?   $??4??6?   ???	#FF9898986?   ?? 5!   
   -   5        $ txtCEP??5!      $ 9??6C   $
??Left=?6Q   $??260,404,0,0q?6l   $
 Wrap=?6?   $	??Top=?6?   $	??292??6?   $??23(?6?   $ 1??6?   $??5??6?   ???	#FF9898986?   ?? 5"   
   -   5        $ 	txtBairro??5"      $ 20??6%   $
??Left=?64   $??260,461,0,0q?6O   $
 Wrap=?6d   $	??Top=?6x   $	??501??6?   $??23(?6?   $ 1??6?   $??6??6?   ???	#FF9898986?   ?? 5#   
   -	   5        $ 	txtCidade??5#      $ 20??6%   $
??Left=?64   $??260,523,0,0q?6O   $
 Wrap=?6d   $	??Top=?6x   $	??501??6?   $??23(?6?   $ 1??6?   $??7??6?   ???	#FF9898986?   ?? 5$   
   -
   5         ??Name$ 	btnSalvar??5$      $??Salvar??6$   $
??Left=?65   $??390,594,0,0q?6P   $	??114??6e   $??17(?6q   $??Normal&?6   $??Normal$?6?   $??Medium"?6?   $??8??6?   $??35??6?   $	??Top=?6?    J? RenderTransformOrigin$ 2.582,16.573&?6  ?? 5%   
   -   5        $ 	btnVoltar??5%      $??Voltar??6$   $
??Left=?65   $??0,-4,0,0q?6P   $??62??6b   $??17(?6m   $??Normal&?6{   $??Normal$?6?   $??Medium"?6?   $??12??6?   $??34??6?   $	??Top=?6?   $ 0.495,0.609&?6  ???	#FFE2C6C66%  ?? 5&   
   -   5        $ Fechar??5&      $??X??6!   $
??Left=?6-   $??
1452,0,0,0q?6H   $	??Top=?6\   $??30??6t   $??20(?6   $
??Bold"?6?   ???	#FFF1E6E6???	#FFFB0000???	#FF290303$??33??6  &   System.Windows.Controls.DataGrid  5'   
   -   5          Name$ dataGrid??5'      $??823,183,33,90q?6%      AutoGenerateColumns .    SelectionMode$ Extended=?6W      SelectionUnit$ FullRow=?6p   6?   ?? 5(   
   -   5        $ 
btnDeletar??5(      $??Deletar??6%   $
??Left=?67   $??921,594,0,0q?6R   $	??107??6g   $??17(?6s   $??Normal&?6?   $??Normal$?6?   $??Medium"?6?   $??9??6?   $??34??6?   $	??Top=?6?   $ 0.495,0.609&?6  ?? 5)   
   -   5        $ 
btnAlterar??5)      $??Alterar??6%   $
??Left=?67   $??1207,593,0,0q?6R   $	??107??6h   $??17(?6t   $??Normal&?6?   $??Normal$?6?   $??Medium"?6?   $??10??6?   $??34??6?   $	??Top=?6?   $ 0.495,0.609&?6	  ?? 5*   
   -   5        $ txtPesquisa??5*      $ 14??6(   $
??Left=?67   $??464,109,0,0q?6R   $
 Wrap=?6g   $	??Top=?6{   $	??501??6?   $??23(?6?   $ 1??6?   $??0??6?   ???	#FF9898986
  ?? 5+   
   -   5        $ btnPesquisar??5+      $??	Pesquisar??6'   $
??Left=?6;   $??1012,107,0,0q?6V   $	??114??6l   $??17(?6x   $??Normal&?6?   $??Normal$?6?   $??Medium"?6?   $??0??6?   $??35??6?   $	??Top=?6?   $ 2.582,16.573&?6  50      52      