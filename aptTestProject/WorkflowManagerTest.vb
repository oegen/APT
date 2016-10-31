Imports aptEntities
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports aptBusinessLogic

'''<summary>
'''This is a test class for PetriNetManagerTest and is intended
'''to contain all PetriNetManagerTest Unit Tests
'''</summary>
<TestClass()> _
Public Class WorkflowManagerTest

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

#Region "Full Workflow Test"

    <TestMethod(), DeploymentItem("aptBusinessLogic.dll")> _
    Public Sub RunWorkflowToEndTest()
        ' Create new project
        Dim newProject As Project = New Project() With {.Description = "unit test to run through entire workflow", _
                                                        .Active = True, .BudgetCode = "234", .IsBDProject = True, _
                                                        .Name = "Unit Test - Thorough Run Through", .RequiredDate = Date.Now, _
                                                        .Stopped = False}

        Dim logMessage As String = ""
        Dim finished As Boolean = False
        Dim timeOutCount As Integer = 0
        Dim totalTimeOut As Integer = 1000

        'ProjectManager.AddNewProject(newProject, 204)

        ' Give user all project roles
        UserManager.GiveUserProjectRole(newProject.ID, 204, 1)
        UserManager.GiveUserProjectRole(newProject.ID, 204, 2)
        UserManager.GiveUserProjectRole(newProject.ID, 204, 3)
        UserManager.GiveUserProjectRole(newProject.ID, 204, 7)
        UserManager.GiveUserProjectRole(newProject.ID, 204, 10)
        UserManager.GiveUserProjectRole(newProject.ID, 204, 12)

        ' Create Elements
        ElementManager_Accessor.AddNewElement(New Element() With {.Name = "Test Element 1", .Active = True, .Project = newProject, _
                                                                  .SubclassType = GetSubclassType(6), .ElementStopped = False})

        ElementManager_Accessor.AddNewElement(New Element() With {.Name = "Test Element 2", .Active = True, .Project = newProject, _
                                                                  .SubclassType = GetSubclassType(6), .ElementStopped = False})

        ElementManager_Accessor.AddNewElement(New Element() With {.Name = "Test Element 3", .Active = True, .Project = newProject, _
                                                                  .SubclassType = GetSubclassType(6), .ElementStopped = False})

        ' While outstanding tasks remain
        Dim tokenList As List(Of Token) = WorkflowManager_Accessor.GetTokensByUserAndStatus(204, 1, newProject.ID)

        While tokenList.Count > 0
            tokenList = WorkflowManager_Accessor.GetTokensByUserAndStatus(204, 1, newProject.ID)

            For Each tokenObj As Token In tokenList

                Console.WriteLine(String.Format("Completing Transition {0} at Place {1}", _
                                                WorkflowManager_Accessor.GetTransitionByToken(tokenObj.ID).ID, _
                                                tokenObj.Place.ID, Environment.NewLine))

                logMessage += String.Format("{2}Completing Transition {0} at Place {1}", WorkflowManager_Accessor.GetTransitionByToken(tokenObj.ID).ID, _
                                            tokenObj.Place.ID, Environment.NewLine)

                WorkflowManager_Accessor.CompleteProcess(tokenObj.ID, "a")

                If GetTransitionByToken(tokenObj.ID).ID = 57 Then
                    finished = True
                End If

            Next

            timeOutCount += 1

            If totalTimeOut <= timeOutCount Then
                Exit While
            End If

        End While

        If finished = True Then
            Assert.AreEqual(True, finished)
        Else
            Assert.Fail(logMessage)
        End If
        
    End Sub

#End Region

    '''<summary>
    ''' LP - 26/05/2011
    '''A test for GetTransitionsOfPlaces
    '''</summary>
    <TestMethod(), DeploymentItem("aptBusinessLogic.dll")> _
    Public Sub GetTransitionsOfPlacesTest()
        Dim placeType As New PlaceType With {.ID = AppSettingsGet.PlaceTypeIntermediate}
        Dim place As New Place With {.ID = 4, .PlaceType = placeType}
        Dim placeList As New List(Of Place) From {place}
        Dim expected As New List(Of Transition) From {New Transition With {.ID = 6}}
        Dim actual As List(Of Transition)

        actual = WorkflowManager_Accessor.GetTransitionsOfPlaces(placeList)
        Assert.AreEqual(expected(0).ID, actual(0).ID)
    End Sub

    '''<summary>
    ''' LP - 26/05/2011
    '''A test for GetPlacesAssociatedwithTokens
    '''</summary>
    <TestMethod(), DeploymentItem("aptBusinessLogic.dll")> _
    Public Sub GetPlacesAssociatedwithTokensTest()
        Dim tokenList As New List(Of Token) From {New Token With {.ID = 4, .TokenStatus = New TokenStatus With {.ID = 1}}}
        Dim expected As New List(Of Place) From {New Place With {.ID = 4}}
        Dim actual As List(Of Place)

        actual = WorkflowManager_Accessor.GetPlacesAssociatedwithTokens(tokenList)

        Assert.AreEqual(expected(0).ID, actual(0).ID)
    End Sub

    '''<summary>
    '''A test for GetTransitionByToken
    '''</summary>
    <TestMethod()> _
    Public Sub GetTransitionByTokenTest()
        Dim tokenId As Integer = 4
        Dim expected As Integer = 6
        Dim actual As Transition

        actual = WorkflowManager.GetTransitionByToken(tokenId)

        Assert.AreEqual(expected, actual.ID)
    End Sub

    '''<summary>
    '''A test for FulfillsTransitionSecurityLookup
    '''</summary>
    <TestMethod(), _
     DeploymentItem("aptBusinessLogic.dll")> _
    Public Sub FulfillsTransitionSecurityLookupTest()
        Dim userId As Integer = 1
        Dim projectId As Integer = 7001
        Dim transitionId As Integer = 6
        Dim expected As Boolean = True
        Dim actual As Boolean

        actual = WorkflowManager_Accessor.FulfillsTransitionSecurityLookup(userId, projectId, transitionId)

        Assert.AreEqual(expected, actual)
    End Sub
End Class
