Imports System.Collections.Generic
Imports aptEntities
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports aptBusinessLogic

'''<summary>
'''This is a test class for ElementManagerTest and is intended
'''to contain all ElementManagerTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ElementManagerTest

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
    '''A test for EditElement
    '''</summary>
    <TestMethod()> _
    Public Sub EditElementTest()
        Dim element As Element = GetElement(1)
        Dim comparison As New Element

        element.Name = "Unit Test Success"

        ElementManager.UpdateElement(element)

        comparison = GetElement(1)

        Assert.AreEqual(element.Name, comparison.Name)
    End Sub

    '''<summary>
    '''A test for GetAllElements
    '''</summary>
    <TestMethod()> _
    Public Sub GetAllElementsTest()
        Dim actual As List(Of Element)
        actual = ElementManager.GetAllElements

        Assert.AreNotEqual(actual.Count, 0)
    End Sub

    '''<summary>
    '''A test for GetElement
    '''</summary>
    <TestMethod()> _
    Public Sub GetElementTest()
        Dim elementId As Integer = 1
        Dim actual As Element

        actual = ElementManager.GetElement(elementId)
        Assert.AreEqual(elementId, actual.ID)
    End Sub

End Class
