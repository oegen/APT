'----------------------------------------------------------------------------------------------
' Filename    : ConstantLibrary.vb
' Description :
'
' Release Initials  Date        Comment
' 1       LP/TL     25/07/2011  First release.
'----------------------------------------------------------------------------------------------

Imports Microsoft.VisualBasic

Public Module ConstantLibrary

#Region "Project Tab Index"

    Public Enum ProjectTabIndex

        DASHBOARD = 0
        INFO = 1
        TASK = 2
        ELEMENTS = 3
        DOCUMENTS = 4
        TAGS = 5

    End Enum

#End Region

#Region "Project Search"

    Public Enum SearchType
        Name = 0
        OwnerName = 1
        Coordinator = 2
        Artworker = 3
        Brand = 4
    End Enum

#End Region

#Region "Project SubNavItems"

    Public Enum ProjectSubNavItems

        EDIT_PROJECT = 0
        RESERVE_PROJECT_TIME = 2
        PROJECT_HISTORY = 4
        ADDITIONAL_ELEMENTS = 6
        VIEW_RESERVED_TIME = 8
        AUDIT_TRAIL = 10
        BBC_ITEMS = 12
        PREMIUM_BRIEF = 14
        KITTING = 16
        COSTINGS = 18

    End Enum

#End Region

#Region "Form Read Only Mode"

    Public Enum FormMode
        ENABLE_SAVE = 1
        DISABLE_SAVE = 2
    End Enum

#End Region

#Region "JQuery Message Box Types"

    Public Enum MessageBoxType
        Success = 0
        ErrorMessage = 1
    End Enum

#End Region

End Module
