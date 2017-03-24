<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="Test_SignalR.Chat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" ng-app="myChatAttempt">
<head id="Head1" runat="server">
    <title>Signalr Chat Messenger</title>
    <script src="Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.signalR-2.2.0.js" type="text/javascript"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
    <script src="Scripts\angular.min.js" type="text/javascript"></script>
</head>
<body ng-controller="ChatController as theChatController">
    <form id="form1" runat="server">

    <script type="text/javascript">
        $(function () {
            var app = angular.module('myChatAttempt', [])
                .controller('ChatController', ['$scope', function($scope) {

                $scope.chatMessage = 'Enter message';
                $scope.chatTarget = '';
                $scope.myName = '';
                
                var clientName = "";
                var IWannaChat = $.connection.myChatHub;

                IWannaChat.client.addMessage = function (message) {
                    $('#listMessages').append('<li>' + message + '</li>');
                };

                IWannaChat.client.alertText = function (myName) {
                    alert(myName);
                };

                $("#SetMyName").click(function () {
                    clientName = $('#txtName').val();
                    IWannaChat.server.setName(clientName);
                });
                
                
                this.setName = function () {
                    IWannaChat.server.setName($scope.myName);
                };
                this.sendMessage = function () {
                    IWannaChat.server.send($scope.chatMessage);
                };
                this.sendMessageToTarget = function () {
                    IWannaChat.server.sendMessageTo($scope.chatTarget, $scope.chatMessage);
                };


            }]);

            




            $.connection.hub.start();
        });
    </script>

    <div >
        <input type="text" ng-model="chatMessage"/>
        <input type="button" ng-click="theChatController.sendMessage()" value="Broadcast to all" /><br/>
        <input type="text" ng-model="myName"/>
        <input type="button" ng-click="theChatController.setName()" value="Set my name" /><br/>
        <input type="text" ng-model="chatTarget"/>
        <input type="button" ng-click="theChatController.sendMessageToTarget()"  value="Send to Target" />
        <ul id="listMessages">
        </ul>
    </div>
    <div>
        
    </div>
    </form>
</bod>
</html>

