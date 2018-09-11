<%@	Page Language="VB" MasterPageFile="~/Default.master" Title="Plotter"
	CodeFile="Default.aspx.vb" Inherits="Default_aspx" %>

<asp:content id="Content1" contentplaceholderid="Main" runat="server">

    <div class="shim column"></div>
	
	<div class="page" id="home" style="left: 0px; top: 0px; height: 333px;">
		<div id="sidebar">
		    r<asp:LoginView
                    ID="LoginView1" runat="server">
                    <AnonymousTemplate>
                        <asp:login id="Login1" runat="server" OnAuthenticate="OnAuthenticate">					
						<layouttemplate>
							<div class="login">
								<h4>
                                    User Login</h4>
								<asp:label runat="server" id="UserNameLabel" CssClass="label" associatedcontrolid="UserName">User Name</asp:label>
								<asp:textbox runat="server"	id="UserName" cssclass="textbox" accesskey="u"  />
								<asp:requiredfieldvalidator	runat="server" id="UserNameRequired" controltovalidate="UserName" validationgroup="Login1" errormessage="User Name is required." tooltip="User Name	is required." >*</asp:requiredfieldvalidator>
								<asp:label runat="server" id="PasswordLabel" CssClass="label" associatedcontrolid="Password">Password</asp:label>
								<asp:textbox runat="server"	id="Password" textmode="Password" cssclass="textbox" accesskey="p"  />
								<asp:requiredfieldvalidator	runat="server" id="PasswordRequired" controltovalidate="Password" validationgroup="Login1" tooltip="Password is	required." >*</asp:requiredfieldvalidator>
								<div><asp:checkbox runat="server" id="RememberMe" text="Remember me	next time"/></div>
								<asp:imagebutton runat="server"	id="LoginButton" CommandName="Login" AlternateText="login" skinid="login" CssClass="button" OnClick="LoginButton_Click" />
								<a href="register.aspx"	class="button"><asp:image id="Image1" runat="server"  AlternateText="Register&#9;a new account" skinid="create" Visible="False"/></a>
								<p><asp:literal	runat="server" id="FailureText"	enableviewstate="False"></asp:literal></p>
							</div>
						</layouttemplate>
					</asp:login>
					</AnonymousTemplate>
					<LoggedInTemplate>
					
					Login ok!<br />
                       
					</LoggedInTemplate>
            <RoleGroups>
                <asp:RoleGroup Roles="super">
                    <ContentTemplate>
                        <ul>
                            <li><a href="Towers.aspx">Towers</a></li>
                            <li><a href="Users.aspx">Users</a></li>
                        </ul>
                    </ContentTemplate>
                </asp:RoleGroup>
                <asp:RoleGroup Roles="admin">
                    <ContentTemplate>
                        <ul>
                            <li><a href="Towers.aspx">Towers</a></li>
                        </ul>
                    </ContentTemplate>
                </asp:RoleGroup>
                <asp:RoleGroup Roles="user">
                    <ContentTemplate>
                        Logged in
                    </ContentTemplate>
                </asp:RoleGroup>
                <asp:RoleGroup Roles="staff">
                    <ContentTemplate>
                        <ul>
                            <li><a href="Towers.aspx">Towers</a></li>
                            <li>Staff Login</li>
                        </ul>
                    </ContentTemplate>
                </asp:RoleGroup>

            </RoleGroups>

					</asp:LoginView>
			<hr />
		</div>
		<div id="content">
			<h3>Plotter: <span runat="server" id="Headerh3"></span></h3>
			<p>This site is designed for Vlocity clients to be able to plot their position 
                relative to the Vlocity Towers. </p>
			<hr	/>
			<div id="whatsnew">
				<h4>Register to login</h4>
				<p>
                    We have found unauthorised persons using this site for non-vlocity plotting. The 
                    site is provided for potential clients of Vlocity only.</p>
                <p>
                    Please register to use the plotter. Once you have registered, email your 
                    username to <a href="mailto:support@Vlocity.co.za">support@Vlocity.co.za</a> and 
                    your account will be authorised.</p>
			</div>
			<div id="coollinks">
			</div>
		</div>
	</div>


</asp:content>
