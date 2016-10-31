Imports System.Collections.Generic

Imports aptEntities

Imports System

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports aptBusinessLogic



'''<summary>
'''This is a test class for AdhocExceptionManagerTest and is intended
'''to contain all AdhocExceptionManagerTest Unit Tests
'''</summary>
<TestClass()> _
Public Class AdhocExceptionManagerTest


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
    '''A test for GetAllBetweenDates
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllBetweenDatesTest()
        Dim startDate As DateTime = New DateTime(2011, 5, 29)
        Dim endDate As DateTime = New DateTime(2011, 6, 3)
        Dim active As Boolean = False
        Dim actual As List(Of Adhoc)

        actual = AdhocExceptionManager.GetAllBetweenDates(startDate, endDate, active)

        Assert.AreEqual(actual.Count, 1)
    End Sub

    '''<summary>
    '''A test for IsSameDay
    '''</summary>
    <TestMethod(), _
     DeploymentItem("aptBusinessLogic.dll")> _
    Public Sub IsSameDayTest()
        Dim original As DateTime = New DateTime(2011, 6, 1)
        Dim comparison As DateTime = New DateTime(2011, 6, 1)
        Dim comparison2 As DateTime = New DateTime(2011, 6, 2)
        Dim expected As Boolean = True
        Dim expected2 As Boolean = False
        Dim actual As Boolean
        Dim actual2 As Boolean

        actual = AdhocExceptionManager_Accessor.IsSameDay(original, comparison)
        actual2 = AdhocExceptionManager_Accessor.IsSameDay(original, comparison2)

        Assert.AreEqual(expected, actual)
        Assert.AreEqual(expected2, actual2)
    End Sub

    '''<summary>
    '''A test for IsUsersTimeFree
    '''</summary>
    <TestMethod()> _
    Public Sub IsUsersTimeFreeTest()
        Dim userId As Integer = 1
        Dim earlyDate As DateTime = New DateTime(2011, 6, 2, 8, 0, 0)
        Dim duringDate As New DateTime(2011, 6, 2, 13, 0, 0)
        Dim afterDate As DateTime = New DateTime(2011, 6, 2, 18, 0, 0)

        Dim actual As Boolean = AdhocExceptionManager.IsUsersTimeFree(userId, earlyDate, afterDate)
        Dim actual2 As Boolean = AdhocExceptionManager.IsUsersTimeFree(userId, earlyDate, duringDate)
        Dim actual3 As Boolean = AdhocExceptionManager.IsUsersTimeFree(userId, earlyDate, earlyDate)
        Dim actual4 As Boolean = AdhocExceptionManager.IsUsersTimeFree(userId, duringDate, duringDate)
        Dim actual5 As Boolean = AdhocExceptionManager.IsUsersTimeFree(userId, duringDate, afterDate)
        Dim actual6 As Boolean = AdhocExceptionManager.IsUsersTimeFree(userId, afterDate, afterDate)

        Assert.AreEqual(False, actual)
        Assert.AreEqual(False, actual2)
        Assert.AreEqual(True, actual3)
        Assert.AreEqual(False, actual4)
        Assert.AreEqual(False, actual5)
        Assert.AreEqual(True, actual6)
    End Sub

End Class
