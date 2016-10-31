Imports System.Collections.Generic

Imports aptEntities

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports aptBusinessLogic



'''<summary>
'''This is a test class for AuditTrailManagerTest and is intended
'''to contain all AuditTrailManagerTest Unit Tests
'''</summary>
<TestClass()> _
Public Class AuditTrailManagerTest


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
    '''A test for PostAudit
    '''</summary>
    <TestMethod()> _
    Public Sub PostAuditTest()
        ' Dim auditToPost As New Audit With {.AuditDate = Date.Now, .Message = "Hello Unit Test", .Project = ProjectManager.GetProject(7001), .UserStr = "Mrs Unit Testy", .ChangeType = AuditTrailManager.GetChangeTypeById(1), .Section = AuditTrailManager.GetAuditSectionById(1)}

        ' AuditTrailManager.PostAudit(auditToPost)
    End Sub
End Class
