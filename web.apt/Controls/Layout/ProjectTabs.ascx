<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectTabs.ascx.vb"
    Inherits="Controls_Layout_ProjectTabs" %>

<nav>
    <div id="tabs" class="group">                
        <div id="tab-nav-wrap" class="group">
            <ul>
                <li><a href="#tabs-1">Dashboard</a></li>
                <li><a href="#tabs-2">Info</a></li>
                <li><a href="#tabs-3">Tasks</a></li>
                <li><a href="#tabs-4">Elements</a></li>
                <li><a href="#tabs-5">Documents</a></li>
                <li><a href="#tabs-6">Tags</a></li>
            </ul>
        </div>
                        
        <div id="tabs-1" class="tabswrap">                       
            <!-- Insert dashboard control -->
        </div>

        <div id="tabs-2" class="tabswrap">
            <!-- Insert project info control -->
        </div>

        <div id="tabs-3" class="tabswrap">
            <section id="tasks" class="group">
                <!-- Insert task -->     
            </section>       
        </div>

        <div id="tabs-4" class="tabswrap">
            <section id="elements" class="group">
                 <!-- Insert element control -->                                             
            </section>     
        </div>

        <div id="tabs-5" class="tabswrap">
            <!-- Insert documents control -->            
        </div>

        <div id="tabs-6" class="tabswrap">
            <!-- Insert tags control -->            
        </div>

    </div>

</nav>
