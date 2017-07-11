using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DForm
{
    /// <summary>
    /// Base class for all questions. It is a composite.
    /// </summary>
    public abstract class QuestionBase
    {
        readonly List<QuestionBase> _children;
        QuestionBase _parent;

        protected QuestionBase( QuestionBase parent )
        {
            _parent = parent;
            _children = new List<QuestionBase>();
        }

        /// <summary>
        /// Gets or sets the title of the question.
        /// </summary>
        public string Title { get; set; }

        public QuestionBase Parent
        {
            get => _parent;
            set
            {
                if( _parent != value )
                {
                    if( _parent != null )
                    {
                        _parent._children.Remove( this );
                    }
                    _parent = value;
                    if( _parent != null )
                    {
                        _parent._children.Add( this );
                    }
                }
            }
        }

        public QuestionBase AddChild( string questionType )
        {
            Type tQuestion = Type.GetType( questionType );
            QuestionBase q = (QuestionBase)Activator.CreateInstance( tQuestion, this );
            _children.Add( q );
            return q;
        }

        public IReadOnlyList<QuestionBase> Children => _children;

        public void RemoveChild( QuestionBase c )
        {
            if( c.Parent == this ) c.Parent = null;
        }

        public virtual Form Form => Parent?.Form;

        public int Index
        {

            get => _parent != null
                            ? _parent._children.IndexOf( this )
                            : -1;
            set
            {
                if( _parent == null ) throw new InvalidOperationException( "Parent is required." );
                if( value < 0 || value >= _parent._children.Count )
                {
                    throw new ArgumentOutOfRangeException();
                }
                int oldIdx = _parent._children.IndexOf( this );
                if( oldIdx != value )
                {
                    _parent._children.RemoveAt( oldIdx );
                    if( oldIdx > value )
                    {
                        _parent._children.Insert( value, this );
                    }
                    else
                    {
                        _parent._children.Insert( value - 1, this );
                    }

                }
            }
        }
    }
}
