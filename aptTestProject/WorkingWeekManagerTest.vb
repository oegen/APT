Imports System.Collections.Generic

Imports aptEntities

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports aptBusinessLogic



'''<summary>
'''This is a test class for WorkingWeekManagerTest and is intended
'''to contain all WorkingWeekManagerTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WorkingWeekManagerTest


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
    '''A test for IsTimeFree
    '''</summary>
    <TestMethod()> _
    Public Sub IsTimeFreeTest()
        Dim userId As Integer = 1
        Dim workingWeekId As Integer = 1
        Dim before As Integer = 8
        Dim during As Integer = 11
        Dim after As Integer = 14

        Dim actual As Boolean = WorkingWeekManager.IsTimeFree(userId, workingWeekId, before, after)
        Dim actual2 As Boolean = WorkingWeekManager.IsTimeFree(userId, workingWeekId, before, during)
        Dim actual3 As Boolean = WorkingWeekManager.IsTimeFree(userId, workingWeekId, before, before)
        Dim actual4 As Boolean = WorkingWeekManager.IsTimeFree(userId, workingWeekId, during, during)
        Dim actual5 As Boolean = WorkingWeekManager.IsTimeFree(userId, workingWeekId, during, after)
        Dim actual6 As Boolean = WorkingWeekManager.IsTimeFree(userId, workingWeekId, after, after)

        Assert.AreEqual(False, actual)
        Assert.AreEqual(False, actual2)
        Assert.AreEqual(True, actual3)
        Assert.AreEqual(False, actual4)
        Assert.AreEqual(False, actual5)
        Assert.AreEqual(True, actual6)
    End Sub

End Class
