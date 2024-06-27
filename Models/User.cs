using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;

namespace ufo_api.Models;

public class User
{
    private long Id { get; set; }
    [Required]
    private int user_id { get; set; }
    [Required]
    private string first_name { get; set; }
    [Required]
    private string second_name { get; set; }
    [Required]
    private string birthday { get; set; }
    [Required]
    private string password { get; set; }

    public User(int user_id, string first_name, string second_name, string birthday, string password)
    {
        this.user_id = user_id;
        this.first_name = first_name;
        this.second_name = second_name;
        this.birthday = birthday;
        this.password = password;
    }

    // public User(int user_id, object first_name, object second_name, object birthday)
    // {
    //     this.user_id = user_id;
    //     first_name1 = first_name;
    //     second_name1 = second_name;
    //     birthday1 = birthday;
    // }

    public string getFirstName()
    {
        return this.first_name;
    }

    public void setFirstName(string first_name)
    {
        this.first_name = first_name;
    }

    public string getSecondName()
    {
        return this.second_name;
    }

    public void setSecondName(string second_name)
    {
        this.second_name = second_name;
    }

    public string getBirthday()
    {
        return this.birthday;
    }

    public void setBirthday(string date)
    {
        this.birthday = date;
    }

    public string getPassword()
    {
        return this.password;
    }

    public void setPassword(string password)
    {
        this.password = password;
    }

}