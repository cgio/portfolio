Do While uint32Start + uint32SearchCounter <= uint32End

VirtualQueryEx(_targetProcessHandle, uint32SearchCounter, _mbi, _mbiSize)

If _mbi.State = MemoryAllocationState.Commit Then 'If region of ram is actively being used by process (commited at least..)

    bytReadBytes = ReadBytes(uint32SearchCounter, uint32Block)
    strReadBytes = System.Text.Encoding.ASCII.GetString(bytReadBytes) 'Convert to ASCII seeing as this program is only intended to work with ASCII searches and ASCII regular expressions
    bytReadBytes = Nothing

    If Len(strReadBytes) > 0 Then
        'ReMatches = Re.Matches(strReadBytes, txtRegEx.Text)
        ReMatches = Re.Matches(strReadBytes, "([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})")
        For Each ReMatch As Match In ReMatches
            If Not strExcludedEmails.Contains(ReMatch.Value) Then

                blnSkipMe1 = False
                blnSkipMe2 = False

                For Each Item In strExcludedPartialEmails 'Check if possible email contains a blacklisted string
                    If InStr(ReMatch.Value, Item) Then
                        blnSkipMe1 = True
                        Exit For
                    End If
                Next

                For Each Item In strValidTLDs 'Check if possible email contains a blacklisted string
                    If Len(ReMatch.Value) + 1 - Len(Item) > 0 Then 'Fixes weird bug
                        If Mid(ReMatch.Value, Len(ReMatch.Value) + 1 - Len(Item), Len(Item)) = Item Then
                            blnSkipMe2 = False
                            Exit For
                        End If
                    End If
                    blnSkipMe2 = True 'The TLD was not found, so this is not a valid email, so we skip
                Next

                If blnSkipMe1 = False And blnSkipMe2 = False Then 'Does possible email contain a blacklisted string or a non-valid TLD?
                    If Len(ReMatch.Value) < 255 Then 'Maximum email length according to IETF                                 
                        intStartPos = InStr(ReMatch.Value, "@")
                        intEndPos = InStrRev(ReMatch.Value, ".")
                        If intEndPos - intStartPos <= 20 Then 'Most domains in email addresses are < 20 characters in length
                            lstResult.Items.Add(ReMatch.Value) 'Report on each match
                        End If
                    End If
                End If
            End If
        Next
    End If

    strReadBytes = ""

End If
