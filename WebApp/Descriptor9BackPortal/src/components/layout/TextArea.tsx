import React from "react";

interface TextAreaProps {
  id?: string;
  placeholder?: string;
  type: string;
  value?: string;
  onChange?: (e: React.ChangeEvent<HTMLTextAreaElement>) => void;
  name?: string;
  style?: React.CSSProperties;
  className?: string;
  cols?: number;
  rows?: number;
  labelStyle?: string;
}

const TextArea: React.FC<TextAreaProps> = ({
  id,
  placeholder,
  value,
  onChange,
  name,
  style,
  className,
  rows,
  cols,
  labelStyle
}) => {
  return (
    <span>
      <textarea
        rows={rows || 3} 
        cols={cols || 50}
        id={id}
        placeholder={placeholder}
        value={value}
        onChange={onChange}
        name={name}
        style={style}
        className={className}
      />
      <label className={labelStyle} htmlFor={id}>{name}</label>
    </span>
  );
};

export default TextArea;