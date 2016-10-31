Imports aptEntities

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports aptBusinessLogic



'''<summary>
'''This is a test class for UserManagerTest and is intended
'''to contain all UserManagerTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UserManagerTest


    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''A test for VerifyUserLogin
    '''</summary>
    <TestMethod()> _
    Public Sub VerifyUserLoginTest()
        Dim username As String = "UnitTest1"
        Dim password As String = "B42FwNBL"
        Dim expected As Boolean = True
        Dim actual As Boolean

        actual = UserManager.VerifyUserLogin(username, password, 1)

        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for UserHasProjectAccess
    '''</summary>
    <TestMethod()> _
    Public Sub UserHasProjectAccessTest()
        Dim userId As Integer = 1
        Dim projectId As Integer = 7001
        Dim expected As Boolean = True
        Dim actual As Boolean

        actual = UserManager.UserHasProjectAccess(userId, projectId)

        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for UserHasRole
    '''</summary>
    <TestMethod(), _
     DeploymentItem("aptBusinessLogic.dll")> _
    Public Sub UserHasGlobalRoleTest()
        Dim userId As Integer = 1
        Dim roleId As Integer = 1
        Dim expected As Boolean = True
        Dim actual As Boolean

        actual = UserManager_Accessor.UserHasGlobalRole(userId, roleId)

        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for UserIsAdmin
    '''</summary>
    <TestMethod()> _
    Public Sub UserIsAdminTest()
        Dim userId As Integer = 4
        Dim expected As Boolean = True
        Dim actual As Boolean

        actual = UserManager.UserIsAdmin(userId)

        Assert.AreEqual(expected, actual)
    End Sub

End Class
