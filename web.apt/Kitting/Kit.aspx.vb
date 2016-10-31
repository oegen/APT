Imports aptBusinessLogic

Partial Class Kitting_Kit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack = False Then

            ctrlKit.NewQuote = True

            If IsNumeric(Request.QueryString("projectId")) Then
                ctrlKit.ProjectId = Request.QueryString("projectId")
                ctrlProjectHeader.ProjectId = Request.QueryString("projectId")
                ctrlSubNavProject.ProjectId = Request.QueryString("projectId")

                If IsNumeric(Request.QueryString("kitId")) Then
                    If KitManager.DoesKitBelongToProject(Request.QueryString("kitId"), Request.QueryString("projectId")) Then
                        ctrlKit.KitId = Request.QueryString("kitId")
                        If KitManager.HasKitBeenFinalised(ctrlKit.ProjectId) = True Then
                            ' Here we can't let the user add another kit
                            ctrlKit.HasKitBeenFinalised = True
                        End If
                    End If
                End If

                If IsNumeric(Request.QueryString("QuoteId")) Then
                    ctrlKit.QuoteId = Request.QueryString("QuoteId")
                End If

                If Request.QueryString("newQuote") Is Nothing Then
                    ctrlKit.NewQuote = False
                End If
            End If
        End If

    End Sub

    Protected Sub ctrlKit_KitSaveSuccess(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlKit.KitSaveSuccess
        CType(Me.Master, MasterPage).DisplayConfirmationMessage("Kit has been saved successfully")
    End Sub

End Class
